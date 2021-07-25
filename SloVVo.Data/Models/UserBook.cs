namespace SloVVo.Data.Models
{
    public class UserBooks
    {
        public int LocationId { get; set; }
        public int BibioId { get; set; }
        public int ShelfId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
