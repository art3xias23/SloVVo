namespace SloVVo.Data.Migrations

{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBorrowIdtouserbookprimarykey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserBooks");
            AddColumn("dbo.UserBooks", "BorrowId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserBooks", new[] { "BorrowId", "LocationId", "BiblioId", "ShelfId", "BookId", "UserId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserBooks");
            DropColumn("dbo.UserBooks", "BorrowId");
            AddPrimaryKey("dbo.UserBooks", new[] { "LocationId", "BiblioId", "ShelfId", "BookId", "UserId", "DateOfBorrowing" });
        }
    }
}
