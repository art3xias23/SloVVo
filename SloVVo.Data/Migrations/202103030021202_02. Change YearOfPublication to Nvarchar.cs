namespace SloVVo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02ChangeYearOfPublicationtoNvarchar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "YearOfPublication", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "YearOfPublication", c => c.Int());
        }
    }
}
