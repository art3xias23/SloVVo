ALTER TABLE [dbo].[UserBooks] ADD [DateOfScheduledReturning] [datetime] NOT NULL DEFAULT '1900-01-01T00:00:00.000'
ALTER TABLE [dbo].[UserBooks] ADD [DateOfActualReturning] [datetime]
ALTER TABLE [dbo].[UserBooks] ADD [IsTaken] [bit] NOT NULL DEFAULT 0
DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.UserBooks')
AND col_name(parent_object_id, parent_column_id) = 'DateOfReturning';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[UserBooks] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[UserBooks] DROP COLUMN [DateOfReturning]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202109271924104_UserMappingsChanges', N'SloVVo.Data.Migrations.Configuration',  0x1F8B0800000000000400ED1DDB6EE4B6F5BD40FF41D0535B381E8FB70F893193C03BBB2E8CAEED85C759B44F062D71C64274994A946323C897E5A19FD45F0875A1C4AB444A1CCD6451E4C54B9187E77E0EC97326FFFBEDBF8B1F5EA3D07981691624F1D29D9F9EB90E8CBDC40FE2EDD2CDD1E69B6FDD1FBEFFF39F161FFDE8D5F942E6BD2BE6E19571B6749F11DA5DCC6699F70C23909D4681972659B241A75E12CD809FCCCECFCEBE9BCDE7338841B81896E32CEEF31805112CFF81FFB94A620FEE500EC29BC48761568FE32FEB12AA730B2298ED800797EE3A4CBE7C494E3F00044EF13A045F91EB5C8601C0A8AC61B8711D10C7090208237AF16306D7284DE2ED7A870740F8F0B68378DE068419AC09B868A7EBD272765ED0326B1712505E9EA1243204387F573367C62F1FC462B7611E66DF47CC66F456505DB270E95EE6E839495D87DFEA6215A6C53496BF95344EAB45270EF5E9A4D105AC32C57F27CE2A0F519EC2650C739482F0C4F99C3F8581F74FF8F690FC04E3659C87218D1C460F7F6306F0D0E734D9C114BDDDC30D83F2B5EF3A3376F58C5FDE2C165656A45DC7E8DDB9EBDC6244C053081B3DA0D8B046490AFF0163980204FDCF00219862315EFBB0E4A4808374C7E26FB227563F6C4AAE73035E3FC1788B9E972EFED375AE8257E893911A8F1FE3005B1E5E84D21C4AF0E4F6BE052FC1B6449BC3E27D92FC94B9CE3D0CCBAFD973B0AB6CA396E4633DE12A4DA2FB246CF0AEC61FD7499E7A050189E4E30348B710B1B82C66AD9E756A5F01C250F78A2587D2BC4F89572228EA9E74FAFB00EF9A684E5E3F6379E802C63C18A2FF34FE632DE03689450DE4F5AE61C014BB351C9C84B45A0453EDD5E342B0BFD772213ADE4A20AB87EDD093EA54F7AA7F4390DE6D2ABBAC63DB68FFA8EB0E49CC1BEA0F89CB93FA43E22C759121362945A700F9D8CE68F1613E080E9AFD2AF3D05D18D5025523D44CE0F0A9C7E5E8908FA6D8E08C2D55C7AF12343585C3A8F922C7A9FD3C2A8AB502328A6464D9F145B36923896E2E45F6B4944D8D4F9E34AD93771772DB1DA4798D251A295EBDEA507A47050C53B553C59A3D6A5DBD658FD2CD35E3EF1E52782D972CD541DE5F0F5241CAF91A2961B3EEF8DCDF3124F3D2A905CFF6EBADEDE4F356F2741BF977370CC2CE3130B0D2C2BBCDFB244D939F4BA750012B861F8248C3E2A5F08A5B2E3F0FA17F0FB1EEC7F6005F7AC5A55A07D46E20D7D903C036479661218410C423DDDAB8C44EEAD9C4BCCF24DD9422D482ACA6B408B15F844C93FB3C2AD3ACB636F6B38772B143FD959E61DA8BF157419AA1784F9774962E37B0C11E35820F3084BB673CF1368F9E0A2D658467678F4BDF4F6196596082259A939F6DDC5B0C446640AE587A20C1A3B6A352E7D571D13BFCE0AEED4E79FFAEF0B63AEEF432CB122F2831A138D59EE058C23EC6BED379D952899D042B2C79EC45831DF69B78EBA5FB3781532A800D852DC0F650C9029DBBBCD3BD8B3F60C343D0B9F4AA07A615C83CE08BDA8279E2B323D84FC3B47090205C61D960CF1FC44874EA41EC053B1076A1CE2D324FB767CD36FC970F7007E3C28D77C962FCFECD361CD3FA78B498514AA5A16BE49CD6A919C23DDA484D130E812DBCE6DCC8823C3B3D9D0B5087EB0B87808EB8949704E6DAC2B173F4EE13E80AE5A755929539ED56AE9553D510AA14A044513414CF903ECAFB77A124CBAC593AEB20A36F14AA08D3CDBEC3FB5E39DE3AFA2C4FC48D4C492E8B719B4FE5755B2DE9F49392D7826E03B0AA124A6414FAA9A9F6C3BDB6C08EB161D6F05ECDEC66CDEC6ECD308408C2F89A5831811532CFAA2AB597D79CB44A4F9E6D8DA29AFCE1D634A11A6444327A7424A5AA6F32525B19DD6337B7AE28D531AD28DAC32B604AF4BABCAB2AAEAA9A723EE1C60B1B647DE995D5A7685E010AC86B8818B2B0BB6C4F869C56091AC402A8BDADB0BCD29F9EC5C413C800B447BD1E20754E2A83D164F13D20A8B021C0A0BE69405141101653926799493D9C5273A42FABBC22F69DCC1B841BB109BADC7716A74050C2E363384B9C2EE18DB414744BCF88FDA74473AAF9732105A155B6D134D3774E22C5AA934EDF5987C2B5D6C70E6A25479B3E6E0D2494BED29213AB3AF6E81C7C38A2FBC5AC38E9F4316FA866534829745B9189EBE4E2E6FA2DA6D03A0C1C403C5B352692AE4E7FFA13200AE5268875102ECD7746283BB94B6D8274F36D31AB6AF1EB81C54C51B4BFB801BB5D106FA922FE7AC4595715FCAB6FD6E695ED510563E6311CE5538A662794A4600BB9AF786B8C69F9FA53A41B4FA0B89C5FF991304D4C49142192ECC7661DA2B848E824F38BBFE9EC87696720398A98BFD5CBAF306D5191FB95CF0782C6880B9DA291028420551680AE92308FE2BE9CB00F52553523C2AAC625E79619479190830AAC138ED5AC24B4E4A4324C332995A9A0B98CE4CB547CA50F95345FBB0F9B2A68ED899386D5750E55416A0EA334A08E13AA12A3FA98CAE0A338BA76431175AF1D35D5631B1641DD29333C525F35AB6149AAA7699892CF47636B6D3A3DD6DE54E7030D9B532F9DC6EED8525619BC23F390CD7960ACD014871B0D9929574E61704C15A804DA9189AB2BCD369397129286C43AD6FE3FBC8D096FE4458586A27A655143114A176970C24753B8B212467103D92CD39D849A46711B618AFE1E4DC9230DB5193C2A8BB763ED030D5DDFC6ED682F55B34703A2860DAC9B14D731D64D06F5E1083570343CE1A341124AEADE981C940C1AE057D6AB31489523D36B317BDF20AA32B94BE957573253E75855DC934894B1EF89C6488B1FD5CFDDD4EE4688299F6F2C86505B01D44EF81C1F3C3BB98DF5C70FCAE7C9EBAC28876D0A333585C9DF97892A2E5C9BF1531A036BAECFB86BB2457D65D5FF0318C21D5635C57530F92F815FDE5FBD65084695F1ACFF13AEC2009B743BE106C4C10666A8AAF676CFCFE6E7DC4F681CCFCF59CCB2CC0F25577E3ABF69711DFBF075E9FEE2FC7AD8DF9D080AE6F7D6AC1B3678883F3511BF80D47B06295394FCF7B3A255AD13F6D89F6F18C7E6E3EECB1AD26C558A5BCDA562D98573FDAFC776E589739762BBBC70CE300FC7766995DB8FEBD11A0282EDD01A0AA1579FCF59751EF23307BAF221EB54D2D1D95BE860D5DDBC59386677E5AF2E68B90A9D1D98A8694621B5549F46F3267DAB2E69B03BB0EDFD65CDF144A87F89C0EB5F2DB151DD713ECEEBDB6C0ADF0B83257DE052A399DB0BAF5DADD55F738C954EB5D7FB6C1E8E1F09031E6BE21E2BC4ED8769BB68CD07A0258BFC76B13A1FC22C3199B08BD4BB01480D0F73235547D174EEE361B48FA6F39180154DE732A83A2906D784FE14F4659663DBABC7F95A3B4DD07B896942DFF3D013A3C48DB0FDCAF6002BFA8CB9F385D63180ED26B69602D35DC10381DAEE606D6B6DA76E32654BF2A62ABDEF280DD86B67EA57D489DA24FA03BB450F23787579C13E5B4CBFA69ED2B16DA4534A5BF15CB997FE476D39F73C024D2469CDEEDA69BB5E2585E993B6354EA92F5D85337FDC1ED943B5C41E465FAC8474C31B15B33B15B35B957D69EF1F85FCE3EA741DDFDC3A65AC55F620ECA98155DBF68EA06355EC22E12526EB46ED6846AD5EF097AEFF9460F956A74445F78FB45355D9A82A03ACD7F5D99E163B5A5865E0D5FD938A0ED7AE0657D906CA5EC581EDAFB23DCCBA6395CDB12AD022D8FDF6CDF2C2648BE2FB1AEB8486B2E3E98CE55488291C3F0459969A5F85CEC5831163B1C1D58C28C178AD77715A6E61ED95C0C4E4596D5295F4A5EE55250DDA50C5BA391CEFA9FFBD14CE34B260DB8228AA01E3CA45B440C99CEB7893909483C3884CE16E7F6F20023E4E032E53146C8087F0670F6659F9EEF10584399EF2317A82FE757C97A35D8E30C9307A0A99DF282D1297AEFDCB5E5B16E7C5DDAE0C823648C06806C5E3CC5DFC3E0F42BFC1FB4AF2B4A200516444F54345214B543C586CDF1A48E2EF59AA00D5EC6B12B90718ED420C2CBB8BD7E0050EC10D1BD527B805DE1B297F5403E91704CBF6C587006C531065358C763DFE27D6613F7AFDFE77E5F2B2C1656D0000 , N'6.4.4')
