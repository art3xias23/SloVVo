namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecifyingcolumnnamesforUserBooks : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserBooks", newName: "UserBooks1");
            CreateTable(
                "dbo.UserBooks",
                c => new
                    {
                        BibioId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        ShelfId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BibioId, t.BookId, t.LocationId, t.ShelfId, t.UserId })
                .ForeignKey("dbo.Books", t => new { t.BibioId, t.BookId, t.LocationId, t.ShelfId }, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.BibioId, t.BookId, t.LocationId, t.ShelfId })
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBooks", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserBooks", new[] { "BibioId", "BookId", "LocationId", "ShelfId" }, "dbo.Books");
            DropIndex("dbo.UserBooks", new[] { "UserId" });
            DropIndex("dbo.UserBooks", new[] { "BibioId", "BookId", "LocationId", "ShelfId" });
            DropTable("dbo.UserBooks");
            RenameTable(name: "dbo.UserBooks1", newName: "UserBooks");
        }
    }
}
