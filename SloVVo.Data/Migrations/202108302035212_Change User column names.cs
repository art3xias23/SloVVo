namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUsercolumnnames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Firstname", c => c.String(nullable: false, maxLength: 4000));
            AddColumn("dbo.Users", "TelephoneNumber", c => c.Int());
            DropColumn("dbo.Users", "Forename");
            DropColumn("dbo.Users", "Telephone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Telephone", c => c.Int());
            AddColumn("dbo.Users", "Forename", c => c.String(nullable: false, maxLength: 4000));
            DropColumn("dbo.Users", "TelephoneNumber");
            DropColumn("dbo.Users", "Firstname");
        }
    }
}
