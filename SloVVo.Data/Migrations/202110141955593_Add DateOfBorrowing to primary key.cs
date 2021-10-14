namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateOfBorrowingtoprimarykey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserBooks");
            AddPrimaryKey("dbo.UserBooks", new[] { "LocationId", "BiblioId", "ShelfId", "BookId", "UserId", "DateOfBorrowing" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserBooks");
            AddPrimaryKey("dbo.UserBooks", new[] { "LocationId", "BiblioId", "ShelfId", "BookId", "UserId" });
        }
    }
}
