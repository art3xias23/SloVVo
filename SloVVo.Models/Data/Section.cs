using System.Collections.Generic;

namespace SloVVo.Models.Data
{
    public class Section
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
