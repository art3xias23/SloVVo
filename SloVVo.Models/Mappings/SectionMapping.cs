using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SloVVo.Models.Data;

namespace SloVVo.Models.Mappings
{
    public class SectionMapping : EntityTypeConfiguration<Section>
    {
        public SectionMapping() : this("dbo")
        {
            
        }

        public SectionMapping(string schema)
        {
            ToTable("Sections", schema);

            HasKey(x => x.SectionId);

            Property(x => x.SectionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.SectionName)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            HasMany(x => x.Books)
                .WithOptional(x => x.Section)
                .HasForeignKey(x => x.SectionId);
        }
    }
}
