using System.Collections.Generic;
using SloVVo.Data.Migrations;

namespace SloVVo.Data.Models
{
    public class Book
    {
        public int LocationId { get; set; }
        public int BiblioId { get; set; }
        public int ShelfId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public bool IsTaken { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<UserBooks> UserBooks { get; set; }
    }
}
