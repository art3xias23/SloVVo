using System.Collections.Generic;

namespace SloVVo.Data.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
