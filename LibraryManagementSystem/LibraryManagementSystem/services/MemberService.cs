using System.Data;
using Microsoft.Data.SqlClient;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utils;
namespace LibraryManagementSystem.Services
{
    public class MemberService
    {
        readonly DB _dB;
        public MemberService()
        {
            _dB = new DB();
        }

        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();
            try
            {
                string query = "SELECT * FROM Members";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Member member = new Member();
                    member.MemberID = Convert.ToInt32(reader["MemberID"]);
                    member.Name = reader["Name"].ToString();
                    member.Surname = reader["Surname"].ToString();
                    member.Email = reader["Email"].ToString();
                    member.Phone = reader["Phone"].ToString();
                    members.Add(member);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting members");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return members;
        }
        public int AddMember(Member member)
        {
            int result = 0;
            try
            {
                string query = "INSERT INTO Members (Name, Surname, Email, Phone) VALUES (@Name, @Surname, @Email, @Phone)";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@Name", member.Name);
                command.Parameters.AddWithValue("@Surname", member.Surname);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.Parameters.AddWithValue("@Phone", member.Phone);
                result = command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Error while adding member");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }
        public int UpdateMember(Member member)
        {
            int result = 0;
            try
            {
                string query = "UPDATE Members SET Name = @Name, Surname = @Surname, Email = @Email, Phone = @Phone WHERE MemberID = @MemberID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@Name", member.Name);
                command.Parameters.AddWithValue("@Surname", member.Surname);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.Parameters.AddWithValue("@Phone", member.Phone);
                command.Parameters.AddWithValue("@MemberID", member.MemberID);
                result = command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Error while updating member");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }
        public int DeleteMember(int memberID)
        {
            int result = 0;
            try
            {
                string query = "DELETE FROM Members WHERE MemberID = @MemberID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@MemberID", memberID);
                result = command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Error while deleting member");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }
        public Member GetMemberById(int memberID)
        {
            Member member = new Member();
            try
            {
                string query = "SELECT * FROM Members WHERE MemberID = @MemberID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@MemberID", memberID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    member.MemberID = Convert.ToInt32(reader["MemberID"]);
                    member.Name = reader["Name"].ToString();
                    member.Surname = reader["Surname"].ToString();
                    member.Email = reader["Email"].ToString();
                    member.Phone = reader["Phone"].ToString();
                }
            }
            catch
            {
                Console.WriteLine("Error while getting member");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return member;
        }

        //Üyeler ve ödünç aldıkları kitapları listeleyen metod
        public List<MembersWithLoans> GetMembersWithLoans()
        {
            List<MembersWithLoans> membersWithLoans = new List<MembersWithLoans>();
            try
            {
                string query = "SELECT* FROM MembersWithLoans";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MembersWithLoans memberWithLoan = new MembersWithLoans();
                    memberWithLoan.MemberID = Convert.ToInt32(reader["MemberID"]);
                    memberWithLoan.Name = reader["Name"].ToString();
                    memberWithLoan.Surname = reader["Surname"].ToString();
                    memberWithLoan.Email = reader["Email"].ToString();
                    memberWithLoan.Phone = reader["Phone"].ToString();
                    memberWithLoan.BookID = Convert.ToInt32(reader["BookID"]);
                    memberWithLoan.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
                    if (reader["ReturnDate"] != DBNull.Value)
                        memberWithLoan.ReturnDate = Convert.ToDateTime(reader["ReturnDate"]);
                    membersWithLoans.Add(memberWithLoan);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting members with loans");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return membersWithLoans;
        }

    }
}