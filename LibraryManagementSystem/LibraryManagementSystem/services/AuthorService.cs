using System.Data;
using Microsoft.Data.SqlClient;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utils;
using System.Linq.Expressions;

namespace LibraryManagementSystem.Services
{

    public class AuthorService
    {
        readonly DB _dB;
        public AuthorService()
        {
            _dB = new DB();
        }

        //Tüm yazarları listeler.
        public List<Author> GetAllAuthors()
        {
            List<Author> authors = new List<Author>();
            try
            {
                string query = "SELECT * FROM Authors";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Author author = new Author();
                    author.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                    author.Name = reader["Name"].ToString();
                    author.Surname = reader["Surname"].ToString();
                    authors.Add(author);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting authors");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return authors;
        }
        //Yazar ekler.
        public int AddAuthor(Author author)
        {
            int result = 0;
            try
            {
                string query = "INSERT INTO Authors (Name, Surname) VALUES (@Name, @Surname); SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@Name", author.Name);
                command.Parameters.AddWithValue("@Surname", author.Surname);
                result = Convert.ToInt32(command.ExecuteNonQuery());
            }
            catch
            {
                Console.WriteLine("Error while adding author");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }

        //Yazar günceller.
        public int UpdateAuthor(Author author)
        {
            int result = 0;
            try
            {
                string query = "UPDATE Authors SET Name=@Name, Surname=@Surname WHERE AuthorID=@AuthorID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@Name", author.Name);
                command.Parameters.AddWithValue("@Surname", author.Surname);
                command.Parameters.AddWithValue("@AuthorID", author.AuthorID);
                result = command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Error while updating author");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }
        //Yazar siler.
        public int DeleteAuthor(int authorID)
        {
            int result = 0;
            try
            {
                string query = "DELETE FROM Authors WHERE AuthorID=@AuthorID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@AuthorID", authorID);
                result = command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Error while deleting author");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }
        //Yazar ID'sine göre yazarı getirir.
        public Author GetAuthorById(int authorID)
        {
            Author author = new Author();
            try
            {
                string query = "SELECT * FROM Authors WHERE AuthorID=@AuthorID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@AuthorID", authorID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    author.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                    author.Name = reader["Name"].ToString();
                    author.Surname = reader["Surname"].ToString();
                }
            }
            catch
            {
                Console.WriteLine("Error while getting author by id");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return author;

        }
        //Yazar ismine göre yazarları getirir.

        public List<Author> GetAuthorsByName(string? name)
        {
            List<Author> authors = new List<Author>();
            try
            {
                string query = "SELECT * FROM Authors WHERE Name=@Name";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@Name", name);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Author author = new Author();
                    author.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                    author.Name = reader["Name"].ToString();
                    author.Surname = reader["Surname"].ToString();
                    authors.Add(author);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting authors by name");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return authors;
        }
        //Yazar ad ve soyadına göre yazar getirir.
        public List<Author> GetAuthorsByNameAndSurname(string? name, string? surname)
        {
            List<Author> authors = new List<Author>();
            try
            {
                string query = "SELECT * FROM Authors WHERE Name=@Name AND Surname=@Surname";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Surname", surname);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Author author = new Author();
                    author.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                    author.Name = reader["Name"].ToString();
                    author.Surname = reader["Surname"].ToString();
                    authors.Add(author);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting authors by name and surname");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return authors;
        }







    }

}
