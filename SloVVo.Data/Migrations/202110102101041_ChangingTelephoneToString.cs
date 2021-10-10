namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingTelephoneToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "TelephoneNumber", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "TelephoneNumber", c => c.Int());
        }
    }
}
