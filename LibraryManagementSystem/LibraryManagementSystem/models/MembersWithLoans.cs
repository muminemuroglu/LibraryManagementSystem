namespace LibraryManagementSystem.Models
{
    public struct MembersWithLoans
    {
        public int MemberID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int BookID { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public MembersWithLoans()
        {

        }
        public MembersWithLoans(int memberID, string name, string surname, string email, string phone, int bookID, DateTime loanDate, DateTime? returnDate = null)
        {
            MemberID = memberID;
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            BookID = bookID;
            LoanDate = loanDate;
            ReturnDate = returnDate;
        }
    }
}