using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloVVo.Models.Data
{
    public class Book
    {
        [DisplayName("Номер на Библиотека")]
        public int BibioId { get; set; }
        public int ShelfId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Section { get; set; }
        public int YearOfPublication { get; set; }
    }
}
