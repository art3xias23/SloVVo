using System.Collections.Generic;

namespace SloVVo.Data.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
