using System;

namespace SloVVo.Data.Models
{
    public class UserBooks
    {
        public int LocationId { get; set; }
        public int BiblioId { get; set; }
        public int ShelfId { get; set; }
        public int BookId { get; set; }
        public DateTime DateOfBorrowing { get; set; }
        public DateTime DateOfScheduledReturning { get; set; }
        public DateTime? DateOfActualReturning { get; set; }
        public int UserId { get; set; }
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
