namespace LibraryManagementSystem.Models
{
    public struct Loan
    {
        public int LoanID { get; set; }
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }


        public Loan()
        {

        }
        public Loan(int loanID, int bookID, int memberID, DateTime loanDate, DateTime? returnDate = null)
        {
            LoanID = loanID;
            BookID = bookID;
            MemberID = memberID;
            LoanDate = loanDate;
            ReturnDate = returnDate;
        }
    }
}