namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        LocationId = c.Int(nullable: false),
                        BiblioId = c.Int(nullable: false),
                        ShelfId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        BookName = c.String(maxLength: 200),
                        AuthorId = c.Int(),
                        SectionId = c.Int(),
                        YearOfPublication = c.String(maxLength: 4000),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => new { t.LocationId, t.BiblioId, t.ShelfId, t.BookId })
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .Index(t => t.LocationId)
                .Index(t => t.AuthorId)
                .Index(t => t.SectionId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        SectionName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.SectionId);
            
            CreateTable(
                "dbo.UserBooks",
                c => new
                    {
                        LocationId = c.Int(nullable: false),
                        BiblioId = c.Int(nullable: false),
                        ShelfId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        DateOfBorrowing = c.DateTime(nullable: false),
                        DateOfReturning = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.LocationId, t.BiblioId, t.ShelfId, t.BookId, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => new { t.LocationId, t.BiblioId, t.ShelfId, t.BookId }, cascadeDelete: true)
                .Index(t => new { t.LocationId, t.BiblioId, t.ShelfId, t.BookId })
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 4000),
                        Surname = c.String(nullable: false, maxLength: 4000),
                        TelephoneNumber = c.Int(),
                        Address = c.String(maxLength: 4000),
                        Town = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.UserBooks", new[] { "LocationId", "BiblioId", "ShelfId", "BookId" }, "dbo.Books");
            DropForeignKey("dbo.UserBooks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Books", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Books", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Books", "LocationId", "dbo.Locations");
            DropIndex("dbo.UserBooks", new[] { "UserId" });
            DropIndex("dbo.UserBooks", new[] { "LocationId", "BiblioId", "ShelfId", "BookId" });
            DropIndex("dbo.Books", new[] { "User_UserId" });
            DropIndex("dbo.Books", new[] { "SectionId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropIndex("dbo.Books", new[] { "LocationId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserBooks");
            DropTable("dbo.Sections");
            DropTable("dbo.Locations");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
