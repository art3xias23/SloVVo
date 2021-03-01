using System.Data.Entity;
using SloVVo.Models.Data;
using SloVVo.Models.Mappings;

namespace SloVVo.Models.Context
{
    public class SloVVoDataContext : DbContext
    {
        public SloVVoDataContext() : base("name=SlovoConnectionString")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Section> Sections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new BookMapping());
            modelBuilder.Configurations.Add(new AuthorMapping());
            modelBuilder.Configurations.Add(new SectionMapping());
        }
    }
}
