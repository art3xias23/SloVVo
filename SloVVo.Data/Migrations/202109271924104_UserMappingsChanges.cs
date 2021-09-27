namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserMappingsChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserBooks", "DateOfScheduledReturning", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserBooks", "DateOfActualReturning", c => c.DateTime());
            AddColumn("dbo.UserBooks", "IsTaken", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserBooks", "DateOfReturning");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserBooks", "DateOfReturning", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserBooks", "IsTaken");
            DropColumn("dbo.UserBooks", "DateOfActualReturning");
            DropColumn("dbo.UserBooks", "DateOfScheduledReturning");
        }
    }
}
