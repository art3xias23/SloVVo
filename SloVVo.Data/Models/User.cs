using System.Collections.Generic;

namespace SloVVo.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public int Telephone { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<UserBooks> UserBooks { get; set; }
    }
}
