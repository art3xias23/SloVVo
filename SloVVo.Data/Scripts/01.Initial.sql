CREATE TABLE [dbo].[Authors] (
    [AuthorId] [int] NOT NULL IDENTITY,
    [AuthorName] [nvarchar](4000) NOT NULL,
    CONSTRAINT [PK_dbo.Authors] PRIMARY KEY ([AuthorId])
)
CREATE TABLE [dbo].[Books] (
    [LocationId] [int] NOT NULL,
    [BiblioId] [int] NOT NULL,
    [ShelfId] [int] NOT NULL,
    [BookId] [int] NOT NULL,
    [BookName] [nvarchar](200),
    [AuthorId] [int],
    [SectionId] [int],
    [YearOfPublication] [nvarchar](4000),
    [User_UserId] [int],
    CONSTRAINT [PK_dbo.Books] PRIMARY KEY ([LocationId], [BiblioId], [ShelfId], [BookId])
)
CREATE INDEX [IX_LocationId] ON [dbo].[Books]([LocationId])
CREATE INDEX [IX_AuthorId] ON [dbo].[Books]([AuthorId])
CREATE INDEX [IX_SectionId] ON [dbo].[Books]([SectionId])
CREATE INDEX [IX_User_UserId] ON [dbo].[Books]([User_UserId])
CREATE TABLE [dbo].[Locations] (
    [LocationId] [int] NOT NULL IDENTITY,
    [LocationName] [nvarchar](max),
    CONSTRAINT [PK_dbo.Locations] PRIMARY KEY ([LocationId])
)
CREATE TABLE [dbo].[Sections] (
    [SectionId] [int] NOT NULL IDENTITY,
    [SectionName] [nvarchar](100) NOT NULL,
    CONSTRAINT [PK_dbo.Sections] PRIMARY KEY ([SectionId])
)
CREATE TABLE [dbo].[UserBooks] (
    [LocationId] [int] NOT NULL,
    [BiblioId] [int] NOT NULL,
    [ShelfId] [int] NOT NULL,
    [BookId] [int] NOT NULL,
    [UserId] [int] NOT NULL,
    [DateOfBorrowing] [datetime] NOT NULL,
    [DateOfReturning] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.UserBooks] PRIMARY KEY ([LocationId], [BiblioId], [ShelfId], [BookId], [UserId])
)
CREATE INDEX [IX_LocationId_BiblioId_ShelfId_BookId] ON [dbo].[UserBooks]([LocationId], [BiblioId], [ShelfId], [BookId])
CREATE INDEX [IX_UserId] ON [dbo].[UserBooks]([UserId])
CREATE TABLE [dbo].[Users] (
    [UserId] [int] NOT NULL IDENTITY,
    [Firstname] [nvarchar](4000) NOT NULL,
    [Surname] [nvarchar](4000) NOT NULL,
    [TelephoneNumber] [int],
    [Address] [nvarchar](4000),
    [Town] [nvarchar](4000),
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY ([UserId])
)
ALTER TABLE [dbo].[Books] ADD CONSTRAINT [FK_dbo.Books_dbo.Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([LocationId]) ON DELETE CASCADE
ALTER TABLE [dbo].[Books] ADD CONSTRAINT [FK_dbo.Books_dbo.Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Sections] ([SectionId])
ALTER TABLE [dbo].[Books] ADD CONSTRAINT [FK_dbo.Books_dbo.Users_User_UserId] FOREIGN KEY ([User_UserId]) REFERENCES [dbo].[Users] ([UserId])
ALTER TABLE [dbo].[Books] ADD CONSTRAINT [FK_dbo.Books_dbo.Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([AuthorId])
ALTER TABLE [dbo].[UserBooks] ADD CONSTRAINT [FK_dbo.UserBooks_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE
ALTER TABLE [dbo].[UserBooks] ADD CONSTRAINT [FK_dbo.UserBooks_dbo.Books_LocationId_BiblioId_ShelfId_BookId] FOREIGN KEY ([LocationId], [BiblioId], [ShelfId], [BookId]) REFERENCES [dbo].[Books] ([LocationId], [BiblioId], [ShelfId], [BookId]) ON DELETE CASCADE
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202109261634532_Initial', N'SloVVo.Data.Migrations.Configuration',  0x1F8B0800000000000400ED1DDB6EE4B6F5BD40FF41D0535B381E8FB70F893193C03BBB2E8CAEED85C759B44F062D71C64274994A946323C897E5A19FD45F0875A1C4AB444A1CCD6451E4C52B9287E77E217926FFFBEDBF8B1F5EA3D07981691624F1D29D9F9EB90E8CBDC40FE2EDD2CDD1E69B6FDD1FBEFFF39F161FFDE8D5F942E6BD2BE6E19571B6749F11DA5DCC6699F70C23909D4681972659B241A75E12CD809FCCCECFCEBE9BCDE7338841B81896E32CEEF31805112CFF81FFB94A620FEE500EC29BC48761567FC723EB12AA730B2298ED800797EE3A4CBE7C494E3F00044EF13A045F91EB5C8601C0A8AC61B8711D10C7090208237AF16306D7284DE2ED7A873F80F0E16D07F1BC0D0833581370D14ED7A5E5ECBCA065D62E24A0BC3C4349640870FEAE66CE8C5F3E88C56EC33CCCBE8F98CDE8ADA0BA64E1D2BDCCD17392BA0EBFD5C52A4C8B692C7F2B699C568B4E1C6AE8A4D105AC32C57F27CE2A0F519EC2650C739482F0C4F99C3F8581F74FF8F690FC04E3659C87218D1C460F8F311FF0A7CF69B283297ABB871B06E56BDF7566ECEA19BFBC592CACAC48BB8ED1BB73D7B9C58880A710367A40B1618D9214FE03C6300508FA9F014230C562BCF661C9490107E98EC5DF644FAC7ED8945CE706BC7E82F1163D2F5DFCA7EB5C05AFD0275F6A3C7E8C036C7978114A7328C193DBFB16BC04DB126D0E8BF749F253E63AF7302C47B3E76057D9462DC9C77AC2559A44F749D8E05D7D7F5C2779EA15042492C107906E21627159CC5A3DEBD4BE0284A1EE154B0EA5799F12AF4450D43DE9F4F701DE35D19CBC7EC6F2D0058C793044FF69FCC75AC06D128B1AC8EB5DC38029766B38380969B508A6DAABC785607FAFE54274BC9540560FDBA127D5A9EE55FF8620BDDB547659C7B6D1FE51D71D929837D41F129727F587C459EA22436C528A4E01F2B19DD1E2C30C080E9A1D9579E82E8C6A81AA116A2670F8D4DFE5E89041536C70C696AAE357099A9AC261D48CC8716A874745B1564046918C2C3BBE68366D24D1CDA5C89E96B2A9F1C993A675F2EE426EBB8334AFB14423C5AB571D4AEFA88061AA76AA58B347ADABB7EC51BAB966FCDD430AAFE592A53AC8FBEB412A48395F23256CD61D9FFB3B86645E3AB5E0D97EBDB59D7CDE4A9E6E23FFEE8641D8390606565A78B7799FA469F273E9142A60C5E78720D2B07829BC7B88553E1E04AFD3838CCBA1A44E444CB14C323B29422DC86A4A8B103B222475DCF0A8A4AEDADAD8A51DCA9B0D750D7A36602F9C5E056986E23D9D87593A47C0A677D4083EC010EE9EF1C4DB3C7A2AB494119E9D3D2E7D3F855966810996684E7EB67144301099016959E981048FDA7E953AAF8E33D5E135B2B63BE5FDBBC2DBEAB8D3CB2C4BBCA0C484E2545B2CB1847D8C7DA7F35CA3123B095658F2D88B063BEC37F1D64BF76F02A754001B0A5B806DFDC6029DBBBCD3BD8B3F60C343D0B9F4AABB9C15C83CE08BDA8279E2B35FB09F8669E12041B8C2B2C19E3F8891E8D483D80B7620EC429D5B649ED9CE9A6DF8910F7007E3C28D77C962FCFECD361CD3FA78B498514AA5A16BA424EAD40CE1C86AA4A609F5560BAF29D1589067A7A77301EA707DE110D01197B21E37D7168E9DA3779F4057283FAD92ACCC69B772AD9CAA8650A500258AA2A17886F451DEBF0B255966CDD25907197DA35045986EF61DDEF7CAF1D6D16779226E644A72598CDB7C2AAFDB6A49A79F941CCC771B8055955022A3D04F4DB51FEEB505768C0DB386475866875866C75886214410C6D7C48A09AC90B9C154A9BDFC7947ABF4E486D428AAC9EF484D13AA414624A3474752AAA744466A2BA37BECE6D615A52AD38AF77178054C895E976755C55155F3724E38F1C206591F7A657515CD2B4001790D11431676976D65C86995A0412C80DADB0ACB2BFDE9594C3C810C405BEAF500A97352198C268BEF0141850D010635A601450541584C499E6526754749CD915E62F28AD85799370837621374B9AF16A74050C2E363384B9C2EE18DB414744B6BC4FE2AD19C6ABE2EA420B4CA369A66FACC49A45855E9F4D53A14AEB53E76502B296DFAB8359050FA484B4EACAAECD1297C38A2FBC5ACA874FA983754B329A414BAADC8C475727173FD1653681D060E209E7DA02592AE4E7FFA13200AE5268875102ECD7746283B394B6D827433B69855CFDEEB0F8B99E27DFCE206EC7641BCA5DECBD75F9C75F5587EF5CDDAFC117954C198790C47F994A2D9092529D8426E146F8D312D6F7F8A74E3091487F32B3F12A6892989224492FDD8AC431417099D647EF1379DFD309D03244711F3B77AF915A62D2A72BFF2FA40D01871A153F42C8010A4CAB796AB24CCA3B82F27EC83543D50116155DF2575CB8CA348C84105D60965352B092D39A90CD34C4A652A682E23F932155FE9A292E66B77B1A982D6569C34ACAE3A5405A9294669401D15AA12A3BA4C65F05194AEDD5044DD6BBF9AEAB10D8BA0CE94191EA98F9AD5B0240F95699892E1A3B1B5369D1E6B6FAAFA40C3E6D44BA7B13BF6D5A80CDE9179C8A61E182B344571A32133E5CA290C8E797029817664E2EA4AB3CDE4A584A421B18EB5FF0F6F63C21BB951A1A1A86E59D450845782343861D0142EF55A50844B0D1E95CDD8B19781A6A26F2576E44FBD7AA301519F0DEC833C4F63EC837CD48723BC22A3E1098306691C7939C66471E4A3017EE58B2F06A9F2CBF45ACC56ECA22A93D3887E752533750A93E2A441A28C7D971C465AFCA8BE30A67637424C7901623108D90A417602D0F8F0D3C96DAC3F7E505EF05D67C583D2E669A3A630F9132751C58583277E4A6360CD011477D0B4A80F7DFA7FAD413805AAA6B80E26FF25F0CB13A0B70CC1A8329EF57FC2551860936E27DC8038D8C00C55EFA5DDF3B3F939F77B0FC7F3DB0BB32CF343C9A199CE0F305CC73E7C5DBABF38BF1EF647128282F9BDAFBE0DDB1CC4DF45885F40EA3D839479D6FBF7B3A2AFAA13F6D8DF1A18C7E6E36E221AD219548A5BCDA562D98573FDAFC776E589739762BBBC70CE300FC7B61495DB8F6B281A02826D271A0AA1579FCF59751ED293AF2B1FB24E251D9DBD85764BDDCD9B85637657FE448096ABD0D981899A6614524BF56934EF28B7EA9206BB03DBDE5FD6C94D84FA9708BCFED5121BD5EDD1E3BCBECD0EE6BD3058D2B42C359AB9BDF0DAD507FC35C758E9547B8DBAE6E1F89130E0B126EEB142DC7E98B68BD67C005AB2C86F17ABF321CC129309BB48BD1B80D4F030375275141DD23EFE8C2C76486BC21BDBFF3BCE95D9E9D2DD4BC8101A73871664122B651B6AED015634C272E9BB5696CDB6BB5ACB30E9B6D581406DB758B68F41A7EE8264DF8C4DF536BCE3EE7AAFAD935F51AB6493470F6C673C8CE0D5F7DFFBEC81FC9A9A1EC7F6394E296DC56DE05E1AF4B4E5DC73C73291A435DB3FA76DCB94BC9C9EB4EF6E4A7DE97AD9F1C76DE23C54CFE661F4C54A48373CB0303BB2303BB4D897F6FE51C83FAE56CCF1DD9753C65AE523F93D75586ADBDE11B4548A6D0EBCC464ED921DDD92D505F9D2F59F122CDFAA4A54B4A7485B29959D9432C07A6D896DB5D8D1632903AF6EF053B460767560CA365036D30DECCF94ED61D6BEA9ECDE548116C1EEB7B1931726FB6ABBAFF34BE8783A9ED64D4E859897CD8720CB5277A6D05A7730622C76609A112518AFF53643CB3D96BD1298983CAB5D9492C6C9BDAAA4419FA4F82C0DC77BEA7F3584338D2CD8B6208AC77671E5225AA064CE75BC4948CAC16144A670A7BF3710011FA70197290A36C04378D8835956DE5A7C01618EA77C8C9EA07F1DDFE56897234C328C9E42E647348BC4A56BFFB21994C57971B72B83A00D12309A4171B57217BFCF83D06FF0BE925CAD28401419517D5151C812151716DBB70692F8838B2A4035FB9A44EE0146BB1003CBEEE23578814370C346F5096E81F7465E17AA81F40B8265FBE24300B62988B21A46BB1EFF13EBB01FBD7EFF3BBD688AF6716B0000 , N'6.4.4')
