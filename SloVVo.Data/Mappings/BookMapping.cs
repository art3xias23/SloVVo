using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SloVVo.Data.Models;

namespace SloVVo.Data.Mappings
{
    internal class BookMapping : EntityTypeConfiguration<Book>
    {
        public BookMapping() : this("dbo")
        {
            
        }

        public BookMapping(string schema)
        {
            ToTable("Books", schema);

            HasKey(item => new {item.BibioId, item.ShelfId, item.BookId, item.LocationId});

            Property(x => x.BibioId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.LocationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.ShelfId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.BookId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnType("int");

            Property(x => x.BookName)
                .HasColumnType("nvarchar")
                .HasMaxLength(200)
                .IsOptional();

            Property(x => x.AuthorId)
                .IsOptional()
                .HasColumnType("int");

            Property(x => x.SectionId)
                .IsOptional()
                .HasColumnType("int");

            Property(x => x.YearOfPublication)
                .IsOptional()
                .HasColumnType("nvarchar");

            HasOptional(x => x.Author)
                .WithMany(x => x.Books);

            HasOptional(x => x.Section)
                .WithMany(x => x.Books);
        }   
    }
}
