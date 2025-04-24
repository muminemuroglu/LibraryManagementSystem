namespace LibraryManagementSystem.Models
{
    public struct Book
    {
        public int? BookID { get; set; }
        public string? Title { get; set; }
        public int? AuthorID { get; set; }
        public int? PublishYear { get; set; }
        public string? ISBN { get; set; }


        public Book()
        {

        }
        public Book(int bookId, string? title, int authorID, int publishYear, string? isbn)
        {
            BookID = bookId;
            Title = title;
            AuthorID = authorID;
            PublishYear = publishYear;
            ISBN = isbn;
        }
    }
}