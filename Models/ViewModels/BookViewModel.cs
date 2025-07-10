namespace LMS_UI.Models.ViewModels
{
    public class BookViewModel
    {
        public int bookId
        { get; set; }
        public string bookTitle
        { get; set; }
        public string bookAuthor
        { get; set; }
        public bool isBorrowed
        { get; set; }
        public string? borrowedBy
        { get; set; }
        public DateTime? borrowedAt
        { get; set; }
        public DateTime? returnedAt
        { get; set; }
    }
}
