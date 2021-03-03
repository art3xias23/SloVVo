namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01Initial : DbMigration
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
                        BibioId = c.Int(nullable: false),
                        ShelfId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        BookName = c.String(maxLength: 200),
                        AuthorId = c.Int(),
                        SectionId = c.Int(),
                        YearOfPublication = c.Int(),
                    })
                .PrimaryKey(t => new { t.BibioId, t.ShelfId, t.BookId })
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .Index(t => t.AuthorId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        SectionName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.SectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Books", "SectionId", "dbo.Sections");
            DropIndex("dbo.Books", new[] { "SectionId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.Sections");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
