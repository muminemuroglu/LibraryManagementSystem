namespace LibraryManagementSystem.Models
{
    public struct OverdueBooks
    {
        public int LoanID { get; set; }
        public string? Title { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public OverdueBooks()
        {

        }
        public OverdueBooks(int loanID, string title, string name, string surname, DateTime loanDate, DateTime returnDate)
        {
            LoanID = loanID;
            Title = title;
            Name = name;
            Surname = surname;
            LoanDate = loanDate;
            ReturnDate = returnDate;
        }
    }
}