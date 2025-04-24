namespace LibraryManagementSystem.Models
{
    public struct LoansByDate
    {
        public int? LoanID { get; set; }
        public int? BookID { get; set; }
        public string? Title { get; set; }
        public int? MemberID { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public LoansByDate(int loanID, int bookID, string title, int memberID, DateTime loanDate, DateTime? returnDate)
        {
            LoanID = loanID;
            BookID = bookID;
            Title = title;
            MemberID = memberID;
            LoanDate = loanDate;
            ReturnDate = returnDate;
        }
    }
}