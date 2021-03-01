using System.Data.Entity.ModelConfiguration;
using SloVVo.Data.Models;

namespace SloVVo.Data.Mappings
{
    internal class AuthorMapping : EntityTypeConfiguration<Author>
    {
        public AuthorMapping(): this("dbo")
        {
        }

        public AuthorMapping(string schema)
        {
            ToTable("Authors", schema);
            HasKey(x => x.AuthorId);

            Property(x => x.AuthorId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption
                    .Identity)
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.AuthorName)
                .HasColumnType("nvarchar")
                .IsRequired();

            HasMany(x => x.Books)
                .WithOptional(x => x.Author)
                .HasForeignKey(x => x.AuthorId);
        }
    }
}
