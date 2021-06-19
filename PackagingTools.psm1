<#
 .Synopsis
 Transform an XML document using an XSL Transform.

 .Description
 Transform an XML document using an XSL Transform.

 .Parameter XmlDocument
 The XML Document to Transform. Typically a configuration file.

 .Parameter XslTransform
 The XslTransform to use.

 .Example
 Convert-Xml -XmlDocument MyDocument.xml -XslTransform MyTransform.xsl
#>
function Convert-Xml {
    param(
        [System.IO.FileInfo] $XmlDocument,
        [System.IO.FileInfo] $XslTransform
    )

    $xmlPath = (Resolve-Path $XmlDocument).Path
    $xslPath = (Resolve-Path $XslTransform).Path
    $tmpPath = [System.IO.Path]::GetTempFileName()

    $transform = New-Object System.Xml.Xsl.XslCompiledTransform
    $transform.Load($xslPath)
    $transform.Transform($xmlPath, $tmpPath)
    Copy-Item -Path $tmpPath -Destination $xmlPath -Force
}

<#
 .Synopsis
 Pack an Application for a Squirrel Release.

 .Description
 Uses NuGet to package an application executable such that it can then be used
 by Squirrel-Releasify. The Nupkg will be placed in the BinDir.
 nuget must exist on your path.

 .Parameter BinDir
 The directory containing the executable, typically bin\Release.

 .Parameter ExeFile
 The application executable being packaged.

 .Parameter Nuspec
 The Nuspec for the package being created.

 .Example
 Squirrel-Pack -BinDir bin\Release -ExeName MyApp.exe -Nuspec MyApp-Squirrel.nuspec
#>
function Write-Package {
    param(
        [System.IO.DirectoryInfo] $BinDir,
        [System.IO.FileInfo] $ExeFile,
        [System.IO.FileInfo] $NuspecFile
    )

    $binDirPath = (Resolve-Path $BinDir).Path
    $exeFilePath = (Resolve-Path $ExeFile).Path
    $nuspecFilePath = (Resolve-Path $NuspecFile).Path

    $version =$([System.Diagnostics.FileVersionInfo]::GetVersionInfo("$exeFilePath").FileVersion)
    nuget pack $nuspecFilePath `
        -Verbosity quiet `
        -Version $version `
        -OutputDirectory $binDirPath `
        -BasePath $binDirPath
	$nupkgFileName = Get-ChildItem -Path $binDirPath -Filter *.nupkg
	$appName = $nupkgFileName.Name.Split('.')[0]
    $nupkgFilePath = (Join-Path -Path "$binDirPath" -ChildPath "$appName.$version.nupkg")
    return $nupkgFilePath
}

<#
 .Synopsis
 Bundles a NuGet Package into a Squirrel Release.

 .Description
 Uses Squirrel.Windows to bundle a NuGet Package into a Squirrel Release.
 squirrel.exe must exist on your path. The Release will be in the Releases
 folder.

 File from prior releases should be copied into a Releases folder prior to
 running Write-Releases.

 .Parameter SquirrelExe
 Path to the squirrel.exe that you wish to use.

 .Parameter Nupkg
 The NuGet Package for the release.

 .Example
 Write-Releases -SquirrelExe C:\tools\squirrel.exe -NupkgFile .\bin\Release\MyApp.1.0.0.nupkg
#>
function Write-Releases {
    param(
        [System.IO.FileInfo] $SquirrelExe,
        [System.IO.FileInfo] $NupkgFile
    )
    $squirrelExePath = (Resolve-Path $SquirrelExe).Path
    $nupkgFilePath = (Resolve-Path $NupkgFile).Path
    $arguments= "--logLevel","Debug","--no-msi","--releasify",$nupkgFilePath
    Start-Process -FilePath $SquirrelExe `
        -ArgumentList $arguments `
        -PassThru | Wait-Process
}

<#
 .Synopsis
 Copy the files from the Update Location to the local Releases directory.

 .Parameter DeployDir
 The Squirrel Update Location

 .Example
 Copy-InReleases -DeployDir "\\apsq05\c$\inetpub\wwwroot\MyApp\ENV\"
#>
function Copy-InReleases {
    param(
        [System.IO.DirectoryInfo] $DeployDir
    )
    mkdir "./Releases" -ErrorAction Ignore
    $releasesDirPath = (Resolve-Path "./Releases").Path;
    Robocopy.exe /MIR $DeployDir $releasesDirPath
}

<#
 .Synopsis
 Copy the files from the local Releases directory to the Update Location.

 .Parameter DeployDir
 The Squirrel Update Location

 .Example
 Copy-OutReleases -DeployDir "\\apsq05\c$\inetpub\wwwroot\MyApp\ENV\"
#>
function Copy-OutReleases {
    param(
        [System.IO.DirectoryInfo] $DeployDir
    )
    $releasesDirPath = (Resolve-Path "./Releases").Path;
    Robocopy.exe /MIR $releasesDirPath $DeployDir
}

Export-ModuleMember -Function Convert-Xml
Export-ModuleMember -Function Write-Package
Export-ModuleMember -Function Copy-InReleases
Export-ModuleMember -Function Write-Releases
Export-ModuleMember -Function Copy-OutReleases
