using System.Data;
using Microsoft.Data.SqlClient;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utils;
using Microsoft.Identity.Client;
namespace LibraryManagementSystem.Services
{
    public class LoanService
    {
        readonly DB _dB;
        public LoanService()
        {
            _dB = new DB();
        }

        //Ödünç alınmış tümkitapların listesini döndürür.
        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();
            try
            {
                string query = "SELECT * FROM Loans";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Loan loan = new Loan();
                    loan.LoanID = Convert.ToInt32(reader["LoanID"]);
                    loan.BookID = Convert.ToInt32(reader["BookID"]);
                    loan.MemberID = Convert.ToInt32(reader["MemberID"]);
                    loan.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
                    loan.ReturnDate = Convert.ToDateTime(reader["ReturnDate"]);
                    loans.Add(loan);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting loans");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return loans;
        }
        // Ödünç verme işlemi
        public int AddLoan(Loan loan)
        {
            int result = 0;
            try
            {
                string query = "INSERT INTO Loans (BookID, MemberID, LoanDate, ReturnDate) VALUES (@BookID, @MemberID, @LoanDate, @ReturnDate); SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@BookID", loan.BookID);
                command.Parameters.AddWithValue("@MemberID", loan.MemberID);
                command.Parameters.AddWithValue("@LoanDate", loan.LoanDate);
                command.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate);
                result = Convert.ToInt32(command.ExecuteNonQuery());
            }
            catch
            {
                Console.WriteLine("Error while adding loan");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }






        // Ödünç güncelleme işlemi
        public int UpdateLoan(Loan loan)
        {
            int result = 0;
            try
            {
                string query = "UPDATE Loans SET BookID = @BookID, MemberID = @MemberID, LoanDate = @LoanDate, ReturnDate = @ReturnDate WHERE LoanID = @LoanID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@BookID", loan.BookID);
                command.Parameters.AddWithValue("@MemberID", loan.MemberID);
                command.Parameters.AddWithValue("@LoanDate", loan.LoanDate);
                command.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate);
                command.Parameters.AddWithValue("@LoanID", loan.LoanID);
                result = command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Error while updating loan");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }
        //Ödünç Id ye göre silme işlemi
        public int DeleteLoan(int loanID)
        {
            int result = 0;
            try
            {
                string query = "DELETE FROM Loans WHERE LoanID = @LoanID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@LoanID", loanID);
                result = command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Error while deleting loan");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }

        public Loan GetLoanById(int loanID)
        {
            Loan loan = new Loan();
            try
            {
                string query = "SELECT * FROM Loans WHERE LoanID = @LoanID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@LoanID", loanID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    loan.LoanID = Convert.ToInt32(reader["LoanID"]);
                    loan.BookID = Convert.ToInt32(reader["BookID"]);
                    loan.MemberID = Convert.ToInt32(reader["MemberID"]);
                    loan.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
                    loan.ReturnDate = Convert.ToDateTime(reader["ReturnDate"]);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting loan");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return loan;
        }

        //İadesi gecikmiş kitapları getiren metod
        public List<OverdueBooks> GetOverdueBooks()
        {
            List<OverdueBooks> overdueBooks = new List<OverdueBooks>();
            try
            {
                string query = " SELECT * FROM OverdueBooksView";

                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    OverdueBooks overdueBook = new OverdueBooks();
                    overdueBook.LoanID = Convert.ToInt32(reader["LoanID"]);
                    overdueBook.Title = reader["Title"].ToString();
                    overdueBook.Name = reader["Name"].ToString();
                    overdueBook.Surname = reader["Surname"].ToString();
                    overdueBook.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
                    overdueBook.ReturnDate = reader["ReturnDate"] != DBNull.Value
                        ? Convert.ToDateTime(reader["ReturnDate"]) :
                        (DateTime?)null;
                    overdueBooks.Add(overdueBook);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting overdue books");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return overdueBooks;
        }
        //Kitap, ödünç ve üye bilgilerini birleştirerek döndüren metod
        public List<BooksLoansMembers> GetBooksLoansMembers()
        {
            List<BooksLoansMembers> booksLoansMembers = new List<BooksLoansMembers>();
            try
            {
                string query = "SELECT * FROM BooksLoansMembersView";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BooksLoansMembers bookLoanMember = new BooksLoansMembers();
                    bookLoanMember.BookID = Convert.ToInt32(reader["BookID"]);
                    bookLoanMember.Title = reader["Title"].ToString();
                    bookLoanMember.LoanID = Convert.ToInt32(reader["LoanID"]);
                    bookLoanMember.MemberID = Convert.ToInt32(reader["MemberID"]);
                    bookLoanMember.Name = reader["Name"].ToString();
                    bookLoanMember.Surname = reader["Surname"].ToString();
                    bookLoanMember.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
                    bookLoanMember.ReturnDate = Convert.ToDateTime(reader["ReturnDate"]);
                    booksLoansMembers.Add(bookLoanMember);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting books loans members");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return booksLoansMembers;
        }

        //Belirtilen tarih aralığına göre ödünç alınan kitapları getiren fonksiyon
        public List<LoansByDate> GetLoansByDate(DateTime startDate, DateTime endDate)
        {
            List<LoansByDate> loansByDate = new List<LoansByDate>();
            try
            {
                string query = "SELECT * FROM dbo.GetLoansByDate(@StartDate, @EndDate)";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LoansByDate loanByDate = new LoansByDate
                    {
                        LoanID = Convert.ToInt32(reader["LoanID"]),
                        BookID = Convert.ToInt32(reader["BookID"]),
                        Title = reader["Title"].ToString(),
                        MemberID = Convert.ToInt32(reader["MemberID"]),
                        LoanDate = Convert.ToDateTime(reader["LoanDate"]),
                        ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : (DateTime?)null
                    };
                    loansByDate.Add(loanByDate);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while getting loans by date: {ex.Message}");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return loansByDate;
        }
    }
}