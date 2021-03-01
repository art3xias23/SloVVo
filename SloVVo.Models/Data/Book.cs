using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloVVo.Models.Data
{
    public class Book
    {
        public int BibioId { get; set; }
        public int ShelfId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int? AuthorId { get; set; }
        public int? SectionId { get; set; }
        public int? YearOfPublication { get; set; }

        public Author Author { get; set; }
        public Section Section { get; set; }
    }
}
