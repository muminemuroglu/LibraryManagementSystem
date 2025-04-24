namespace LibraryManagementSystem.Models
{
    public struct BooksLoansMembers
    {
        public int? BookID { get; set; }
        public string? Title { get; set; }
        public int? LoanID { get; set; }
        public int? MemberID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public BooksLoansMembers(int bookID, string title, int loanID, int memberID, string name, string surname, DateTime loanDate, DateTime? returnDate)
        {
            BookID = bookID;
            Title = title;
            LoanID = loanID;
            MemberID = memberID;
            Name = name;
            Surname = surname;
            LoanDate = loanDate;
            ReturnDate = returnDate;
        }
    }
}