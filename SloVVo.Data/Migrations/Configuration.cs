using System.Data.Entity.Migrations;
using SloVVo.Data.Context;

namespace SloVVo.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SloVVoDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SloVVoDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
