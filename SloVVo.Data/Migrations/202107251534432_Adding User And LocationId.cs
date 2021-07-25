namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserAndLocationId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Books");
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Forename = c.String(nullable: false, maxLength: 4000),
                        Surname = c.String(nullable: false, maxLength: 4000),
                        Telephone = c.Int(),
                        Address = c.String(nullable: false, maxLength: 4000),
                        Location = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserBooks",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Book_BibioId = c.Int(nullable: false),
                        Book_ShelfId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                        Book_LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Book_BibioId, t.Book_ShelfId, t.Book_BookId, t.Book_LocationId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => new { t.Book_BibioId, t.Book_ShelfId, t.Book_BookId, t.Book_LocationId }, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => new { t.Book_BibioId, t.Book_ShelfId, t.Book_BookId, t.Book_LocationId });
            
            AddColumn("dbo.Books", "LocationId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Books", new[] { "BibioId", "ShelfId", "BookId", "LocationId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBooks", new[] { "Book_BibioId", "Book_ShelfId", "Book_BookId", "Book_LocationId" }, "dbo.Books");
            DropForeignKey("dbo.UserBooks", "User_UserId", "dbo.Users");
            DropIndex("dbo.UserBooks", new[] { "Book_BibioId", "Book_ShelfId", "Book_BookId", "Book_LocationId" });
            DropIndex("dbo.UserBooks", new[] { "User_UserId" });
            DropPrimaryKey("dbo.Books");
            DropColumn("dbo.Books", "LocationId");
            DropTable("dbo.UserBooks");
            DropTable("dbo.Users");
            AddPrimaryKey("dbo.Books", new[] { "BibioId", "ShelfId", "BookId" });
        }
    }
}
