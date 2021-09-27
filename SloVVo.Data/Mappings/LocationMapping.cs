using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SloVVo.Data.Models;

namespace SloVVo.Data.Mappings
{
    public class LocationMapping : EntityTypeConfiguration<Location>
    {
        public LocationMapping() : this("dbo")
        {
            
        }

        public LocationMapping(string schema)
        {
            ToTable("Locations");
            HasKey(x => x.LocationId);

            Property(x => x.LocationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.LocationName)
                .IsRequired()
                .HasColumnType("nvarchar");
        }
    }
}
