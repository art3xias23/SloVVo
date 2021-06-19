using System;
using System.Threading.Tasks;
using NLog;
using Squirrel;
using System.Threading.Tasks;

namespace SloVVo.Logic.Squirrel
{
    public class SquirrelApplication : IDisposable
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private bool _isDisposed;
        private readonly UpdateManager _updateManager;

        public SquirrelApplication(string appUpdateLocation)
        {
            _logger.Trace($"Update Location: {appUpdateLocation}");
            _updateManager = new UpdateManager(appUpdateLocation);
        }

        private void AddShortcuts()
        {
            _logger.Trace($"Enter {nameof(AddShortcuts)}");
            SquirrelAwareApp.HandleEvents(
                onInitialInstall: v => _updateManager.CreateShortcutForThisExe(),
                onAppUpdate: v => _updateManager.CreateShortcutForThisExe(),
                onAppUninstall: v => _updateManager.RemoveShortcutForThisExe(),
                onAppObsoleted: v => _updateManager.RemoveShortcutForThisExe());
        }

        public async Task<(bool success, string message)> CheckForUpdates()
        {
            try
            {
                AddShortcuts();
                var updateInfo = await CheckForUpdateLoop();
                if (updateInfo == null)
                {
                    return (false, "No updates found.");
                }
                await ApplyUpdates(updateInfo);
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return (false, ex.Message);
            }
        }

        private async Task<UpdateInfo> CheckForUpdateLoop()
        {
            UpdateInfo updateInfo;
            while (true)
            {
                _logger.Trace("Checking for updates.");
                updateInfo = await _updateManager.CheckForUpdate();
                if (ReleaseToApply(updateInfo) || _isDisposed)
                {
                    break;
                }
                await Task.Delay(millisecondsDelay: 10_000);
            }
            return updateInfo;
        }

        private bool ReleaseToApply(UpdateInfo updateInfo)
        {
            try
            {
                var currentVersion = updateInfo.CurrentlyInstalledVersion.Version;
                var futureVersion = updateInfo.FutureReleaseEntry.Version;
                var releaseToApply = futureVersion > currentVersion;
                return releaseToApply;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        private async Task ApplyUpdates(UpdateInfo updateInfo)
        {
            if (!ReleaseToApply(updateInfo))
            {
                throw new InvalidOperationException(
                    "Attempted to apply releases when there were none to apply.");
            }

            _logger.Debug("Downloading updates.");
            await _updateManager.DownloadReleases(updateInfo.ReleasesToApply);
            _logger.Debug("Applying updates.");
            await _updateManager.ApplyReleases(updateInfo);
            // Below Will update Release Version in control Penal
            await _updateManager.CreateUninstallerRegistryEntry();
        }

        public void Dispose()
        {
            _logger.Debug($"Enter {nameof(Dispose)}");
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;
            _logger.Trace("Disposing of Update Manager");
            _updateManager?.Dispose();
        }
    }
}