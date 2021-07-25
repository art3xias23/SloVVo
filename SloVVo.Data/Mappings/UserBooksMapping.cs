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
            HasKey(x => new { x.BibioId, x.BookId, x.LocationId, x.ShelfId, x.UserId });

            HasRequired(x => x.User)
                .WithMany(x => x.UserBooks)
                .HasForeignKey(x => x.UserId);

            HasRequired(x => x.Book)
                .WithMany(x => x.UserBooks)
                .HasForeignKey(x => new {x.BibioId, x.BookId, x.LocationId, x.ShelfId});
        }

    }
}
