namespace LibraryManagementSystem.Models
{
    public struct Member
    {
        public int MemberID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public Member()
        {

        }
        public Member(int memberID, string? name, string? surname, string? email, string? phone)
        {
            MemberID = memberID;
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
        }
    }
}