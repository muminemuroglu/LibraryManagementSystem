using System.Data;
using Microsoft.Data.SqlClient;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utils;
namespace LibraryManagementSystem.Services

{
    public class BookService
    {
        readonly DB _dB;
        public BookService()
        {
            _dB = new DB();
        }
        //Tüm kitapların listesini döndürür.
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                string query = "select *from Books";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookID = Convert.ToInt32(reader["BookID"]);
                    book.Title = reader["Title"].ToString();
                    book.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                    book.PublishYear = Convert.ToInt32(reader["PublishYear"]);
                    book.ISBN = reader["ISBN"].ToString();
                    books.Add(book);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting books");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return books;
        }

        //Kitap ekleme işlemi
        public int AddBook(Book book)
        {
            int result = 0;
            try
            {
                string query = "INSERT INTO Books (Title, PublishYear, ISBN) VALUES (@Title, @PublishYear, @ISBN); SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@PublishYear", book.PublishYear);
                command.Parameters.AddWithValue("@ISBN", book.ISBN);
                result = Convert.ToInt32(command.ExecuteNonQuery());
            }
            catch
            {
                Console.WriteLine("Error while adding book");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }
        //Kitap güncelleme işlemi
        public int UpdateBook(Book book)
        {
            int result = 0;
            try
            {
                string query = "UPDATE Books SET Title = @Title, AuthorID = @AuthorID, PublishYear = @PublishYear, ISBN = @ISBN WHERE BookID = @BookID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@AuthorID", book.AuthorID);
                command.Parameters.AddWithValue("@PublishYear", book.PublishYear);
                command.Parameters.AddWithValue("@ISBN", book.ISBN);
                command.Parameters.AddWithValue("@BookID", book.BookID);
                result = command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Error while updating book");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }
        //Kitap silme işlemi
        public int DeleteBook(int bookID)
        {
            int result = 0;
            try
            {
                string query = "DELETE FROM Books WHERE BookID = @BookID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@BookID", bookID);
                result = command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Error while deleting book");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return result;
        }

        //Kitap ID'sine göre kitap bilgilerini döndürür.
        public Book GetBookById(int bookID)
        {
            Book book = new Book();
            try
            {
                string query = "SELECT * FROM Books WHERE BookID = @BookID";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@BookID", bookID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    book.BookID = Convert.ToInt32(reader["BookID"]);
                    book.Title = reader["Title"].ToString();
                    book.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                    book.PublishYear = Convert.ToInt32(reader["PublishYear"]);
                    book.ISBN = reader["ISBN"].ToString();
                }
            }
            catch
            {
                Console.WriteLine("Error while getting book");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return book;
        }
        //Kitap başlığına göre kitapları döndürür.
        public List<Book> GetBooksByTitle(string? title)
        {
            List<Book> books = new List<Book>();
            try
            {
                string query = "SELECT * FROM Books WHERE Title LIKE @Title";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                command.Parameters.AddWithValue("@Title", "%" + title + "%");
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookID = Convert.ToInt32(reader["BookID"]);
                    book.Title = reader["Title"].ToString();
                    book.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                    book.PublishYear = Convert.ToInt32(reader["PublishYear"]);
                    book.ISBN = reader["ISBN"].ToString();
                    books.Add(book);
                }
            }
            catch
            {
                Console.WriteLine("Error while getting books by title");
            }
            finally
            {
                _dB.CloseConnection();
            }
            return books;

        }

        //Bu metod, kitapların yazarlarıyla birlikte döndürülmesini sağlar.
        public List<BookWithAuthors> GetBookWithAuthors()
        {
            List<BookWithAuthors> bookWithAuthorsList = new List<BookWithAuthors>();
            try
            {
                string query = "SELECT* FROM BookWithAuthors";
                SqlCommand command = new SqlCommand(query, _dB.GetConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BookWithAuthors bookWithAuthor = new BookWithAuthors();
                    bookWithAuthor.BookID = Convert.ToInt32(reader["BookID"]);
                    bookWithAuthor.Title = reader["Title"].ToString();
                    bookWithAuthor.PublishYear = Convert.ToInt32(reader["PublishYear"]);
                    bookWithAuthor.ISBN = reader["ISBN"].ToString();
                    bookWithAuthor.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                    bookWithAuthor.Name = reader["Name"].ToString();
                    bookWithAuthor.Surname = reader["Surname"].ToString();
                    bookWithAuthorsList.Add(bookWithAuthor);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                _dB.CloseConnection();
            }
            return bookWithAuthorsList;
        }

        //Girilen paremetre değerine göre kitaplar tablosunu 10'arlı olarak getiren prosedür.
        public List<Book> GetBooksByPage(int page)
        {
            List<Book> books = new List<Book>();

            try
            {

                SqlCommand command = new SqlCommand()
                {
                    CommandText = "pro_Books",
                    CommandType = CommandType.StoredProcedure,
                    Connection = _dB.GetConnection()
                };
                SqlParameter pageParam = new SqlParameter()
                {
                    ParameterName = "@page",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = page
                };
                command.Parameters.Add(pageParam);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookID = Convert.ToInt32(reader["BookID"]);
                    book.Title = reader["Title"].ToString();
                    book.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                    book.PublishYear = Convert.ToInt32(reader["PublishYear"]);
                    book.ISBN = reader["ISBN"].ToString();
                    books.Add(book);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                _dB.CloseConnection();
            }
            return books;
        }
    }
}

