using System.Collections.Generic;

namespace SloVVo.Data.Models
{
    public class Book
    {
        public int LocationId { get; set; }
        public int BibioId { get; set; }
        public int ShelfId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int? AuthorId { get; set; }
        public int? SectionId { get; set; }
        public string YearOfPublication { get; set; }

        public virtual Author Author { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<UserBooks> UserBooks { get; set; }
    }
}
