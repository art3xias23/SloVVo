using System.Data.Entity.Migrations;
using SloVVo.Data.Context;
using SloVVo.Data.Models;

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
            context.Locations.AddOrUpdate(x=>x.LocationId,
                new Location(){LocationId = 1, LocationName = "Основна"},
                new Location() { LocationId = 2, LocationName = "Дарителска" },
            new Location() { LocationId = 3, LocationName = "Коридор" });

            base.Seed(context);
        }
    }
}
