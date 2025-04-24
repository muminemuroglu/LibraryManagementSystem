namespace LibraryManagementSystem.Models
{
    public struct Author
    {
        public int AuthorID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public Author(int authorID, string? name, string? surname)
        {
            AuthorID = authorID;
            Name = name;
            Surname = surname;
        }
    }
}