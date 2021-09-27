using System.Data.Entity.ModelConfiguration;
using SloVVo.Data.Models;

namespace SloVVo.Data.Mappings
{
    public class UserBooksMapping : EntityTypeConfiguration<UserBooks>
    {
        public UserBooksMapping() : this("dbo")
        {

        }

        public UserBooksMapping(string schema)
        {
            ToTable("UserBooks", schema);
            HasKey(x => new { x.LocationId ,x.BiblioId,x.ShelfId, x.BookId, x.UserId });

            HasRequired(x => x.User)
                .WithMany(x => x.UserBooks)
                .HasForeignKey(x => x.UserId);

            HasRequired(x => x.Book)
                .WithMany(x => x.UserBooks)
                .HasForeignKey(x => new { x.LocationId, x.BiblioId, x.ShelfId, x.BookId});

            Property(x => x.DateOfBorrowing)
                .IsRequired()
                .HasColumnType("datetime");

            Property(x => x.DateOfScheduledReturning)
                .IsRequired()
                .HasColumnType("datetime");

            Property(x => x.DateOfActualReturning)
                .IsOptional()
                .HasColumnType("datetime");

            Property(x => x.IsTaken)
                .IsRequired()
                .HasColumnType("bit");
        }

    }
}
