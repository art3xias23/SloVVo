namespace SloVVo.Data.Models
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
