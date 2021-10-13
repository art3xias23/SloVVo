namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIsTaken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "IsTaken", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserBooks", "IsTaken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserBooks", "IsTaken", c => c.Boolean(nullable: false));
            DropColumn("dbo.Books", "IsTaken");
        }
    }
}
