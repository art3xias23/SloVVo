using System.Collections.Generic;

namespace SloVVo.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int? TelephoneNumber { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<UserBooks> UserBooks { get; set; }
    }
}
