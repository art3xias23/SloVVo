namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveContentyearofpublicationandchangeauthortonvarchar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "SectionId", "dbo.Sections");
            DropIndex("dbo.Books", new[] { "SectionId" });
            RenameColumn(table: "dbo.Books", name: "AuthorId", newName: "Author_AuthorId");
            RenameIndex(table: "dbo.Books", name: "IX_AuthorId", newName: "IX_Author_AuthorId");
            AddColumn("dbo.Books", "AuthorName", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Authors", "AuthorName", c => c.String());
            DropColumn("dbo.Books", "SectionId");
            DropColumn("dbo.Books", "YearOfPublication");
            DropTable("dbo.Sections");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        SectionName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.SectionId);
            
            AddColumn("dbo.Books", "YearOfPublication", c => c.String(maxLength: 4000));
            AddColumn("dbo.Books", "SectionId", c => c.Int());
            AlterColumn("dbo.Authors", "AuthorName", c => c.String(nullable: false, maxLength: 4000));
            DropColumn("dbo.Books", "AuthorName");
            RenameIndex(table: "dbo.Books", name: "IX_Author_AuthorId", newName: "IX_AuthorId");
            RenameColumn(table: "dbo.Books", name: "Author_AuthorId", newName: "AuthorId");
            CreateIndex("dbo.Books", "SectionId");
            AddForeignKey("dbo.Books", "SectionId", "dbo.Sections", "SectionId");
        }
    }
}
