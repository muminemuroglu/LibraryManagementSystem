namespace LibraryManagementSystem.Models
{
    public struct BookWithAuthors
    {
        public int BookID { get; set; }
        public string? Title { get; set; }
        public int? PublishYear { get; set; }
        public string? ISBN { get; set; }
        public int AuthorID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }


        public BookWithAuthors()
        {
        }
        public BookWithAuthors(int bookID, string title, int publishYear, string isbn, int authorID, string name, string surname)
        {
            BookID = bookID;
            Title = title;
            PublishYear = publishYear;
            ISBN = isbn;
            AuthorID = authorID;
            Name = name;
            Surname = surname;
        }
    }
}