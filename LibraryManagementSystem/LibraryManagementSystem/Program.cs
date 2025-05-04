using System;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Models;




namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BookService bookService = new BookService();
            AuthorService authorService = new AuthorService();
            LoanService loanService = new LoanService();
            MemberService memberService = new MemberService();


            while (true)
            {
                Console.WriteLine("--------------KÜTÜPHANE YÖNETİM SİSTEMİ-----------------");
                Console.WriteLine("1. Kitap İşlemleri");
                Console.WriteLine("2. Yazar İşlemleri");
                Console.WriteLine("3. Ödünç İşlemleri");
                Console.WriteLine("4. Üye İşlemleri");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminizi yapın: ");

                string? secim = Console.ReadLine();

                if (string.IsNullOrEmpty(secim))
                {
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin!");
                    continue;
                }
                switch (secim)
                {
                    case "1":
                        ShowBookMenu(bookService);
                        break;
                    case "2":
                        ShowAuthorMenu(authorService);
                        break;
                    case "3":
                        ShowLoansMenu(loanService);
                        break;
                    case "4":
                        ShowMemberMenu(memberService);
                        break;
                    case "5":
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                        break;
                }
            }
        }
        static void ShowBookMenu(BookService bookService)
        {
            while (true)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("---KİTAP İŞLEMLERİ---");
                Console.WriteLine("1. Kitap Ekle");
                Console.WriteLine("2. Tüm Kitapları Listele");
                Console.WriteLine("3. Kitapları Güncelle");
                Console.WriteLine("4. Kitap Silme");
                Console.WriteLine("5. Kitap ID'ye Göre Arama");
                Console.WriteLine("6. Kitap İsmine Göre Arama");
                Console.WriteLine("7. Kitap Bilgilerini Yazar Bilgileri İle Beraber Listeleme");
                Console.WriteLine("8. Sayfaya Göre Kitap Listeleme");
                Console.WriteLine("9. Ana Menüye Dön...");
                Console.Write("Seçiminizi yapın: ");

                string? secim = Console.ReadLine();
                if (string.IsNullOrEmpty(secim))
                {
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin!");
                    return;
                }
                switch (secim)
                {
                    case "1":
                        {
                            Console.WriteLine("Kitap eklemek için bilgileri giriniz.");
                            Console.Write("Kitap Adı: ");
                            string? title = Console.ReadLine();
                            Console.Write("Yayın Yılı: ");
                            int publishYear = Convert.ToInt32(Console.ReadLine());
                            Console.Write("ISBN: ");
                            string? isbn = Console.ReadLine();
                            Console.Write("Yazar ID: ");
                            int authorID = Convert.ToInt32(Console.ReadLine());
                            Book book = new Book(0, title, authorID, publishYear, isbn);
                            int result = bookService.AddBook(book);
                            if (result > 0)
                            {
                                Console.WriteLine("Kitap başarıyla eklendi.");
                            }
                            else
                            {
                                Console.WriteLine("Kitap eklenemedi.");
                            }
                            break;

                        }
                    case "2":
                        {
                            Console.WriteLine("KİTAP LİSTESİ:  ");
                            List<Book> books = bookService.GetAllBooks();
                            if (books.Count > 0)
                            {
                                foreach (var item in books)
                                {
                                    Console.WriteLine(item.BookID + " " + item.Title + " " + item.AuthorID + " " + item.PublishYear + " " + item.ISBN);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Kayıtlı kitap bulunamadı.");
                            }
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Güncellemek istediğiniz kitabın ID'sini giriniz.");
                            int bookID = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Kitap Adı: ");
                            string? title = Console.ReadLine();
                            Console.Write("Yazar ID: ");
                            int authorID = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Yayın Yılı: ");
                            int publishYear = Convert.ToInt32(Console.ReadLine());
                            Console.Write("ISBN: ");
                            string? isbn = Console.ReadLine();
                            Book bookadd = new Book(bookID, title, authorID, publishYear, isbn);
                            int result = bookService.UpdateBook(bookadd);
                            if (result > 0)
                            {
                                Console.WriteLine("Kitap bilgileri başarıyla güncellendi.");
                            }
                            else
                            {
                                Console.WriteLine("Kitap bilgileri güncellenemedi.");
                            }
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("KİTAP SİLME İŞLEMİ:  ");
                            Console.WriteLine("Silmek istediğiniz kitabın ID'sini giriniz.");
                            int bookID = Convert.ToInt32(Console.ReadLine());
                            int result = bookService.DeleteBook(bookID);
                            if (result > 0)
                            {
                                Console.WriteLine("Kitap başarıyla silindi.");
                            }
                            else
                            {
                                Console.WriteLine("Kitap silinemedi.");
                            }
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("Bilgilerini istediğiniz kitabın ID'sini giriniz.");
                            int bookID = Convert.ToInt32(Console.ReadLine());
                            Book book = bookService.GetBookById(bookID);
                            if (book.BookID != 0)
                            {
                                Console.WriteLine(book.BookID + " " + book.Title + " " + book.AuthorID + " " + book.PublishYear + " " + book.ISBN);
                            }
                            else
                            {
                                Console.WriteLine("Aranılan kitap bulunamadı.");
                            }

                            break;
                        }
                    case "6":
                        {
                            Console.WriteLine("KİTAP ADINA GÖRE BİLGİLERİNİ GETİRME İŞLEMİ:  ");
                            Console.WriteLine("Bilgilerini görmek istediğiniz kitabın ismini yazınız: ");
                            string? title = Console.ReadLine();
                            List<Book> books = bookService.GetBooksByTitle(title);
                            if (books.Count > 0)
                            {
                                foreach (var item in books)
                                {
                                    Console.WriteLine(item.BookID + " " + item.Title + " " + item.AuthorID + " " + item.PublishYear + " " + item.ISBN);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Aradığınız kitap bulunamadı.");
                            }
                            break;
                        }
                    case "7":
                        {
                            List<BookWithAuthors> books = bookService.GetBookWithAuthors();
                            if (books.Count > 0)
                            {
                                foreach (var item in books)
                                {
                                    Console.WriteLine(item.BookID + " " + item.Title + " " + item.PublishYear + " " + item.ISBN + " " + item.AuthorID + " " + item.Name + " " + item.Surname);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Kayıtlı bir kitap bulunamadı.");
                            }
                            break;
                        }
                    case "8":
                        {
                            Console.WriteLine("KİTAP LİSTELEME İŞLEMİ:  ");
                            Console.WriteLine("Kitap listesinin gitmek istediğiniz sayfa numarasını giriniz: ");
                            int page = Convert.ToInt32(Console.ReadLine());
                            List<Book> books = bookService.GetBooksByPage(page);
                            if (books.Count > 0)
                            {
                                foreach (var item in books)
                                {
                                    Console.WriteLine(item.BookID + " " + item.Title + " " + item.AuthorID + " " + item.PublishYear + " " + item.ISBN);
                                }
                            }
                            else
                            {
                                Console.WriteLine("İlgili sayfada kitap kaydı bulunamadı.");

                            }
                            break;
                        }
                    case "9":
                        {
                            Console.WriteLine("Ana menüye dönülüyor...");
                            return;
                        }

                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }

            }
        }

        static void ShowAuthorMenu(AuthorService authorService)
        {
            while (true)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("---YAZAR İŞLEMLERİ---");
                Console.WriteLine("1. Yazar Ekle");
                Console.WriteLine("2. Tüm Yazarları Listele");
                Console.WriteLine("3. Yazar Güncelle");
                Console.WriteLine("4. Yazar Silme");
                Console.WriteLine("5. Yazar ID'sine göre yazar bilgilerini getir");
                Console.WriteLine("6. Yazar ismine göre yazar bilgilerini getir");
                Console.WriteLine("7. Yazar ad ve soyadına göre yazar bilgilerini getir");
                Console.WriteLine("8. Ana Menüye Dön...");
                Console.Write("Seçiminizi yapın: ");

                string? secim = Console.ReadLine();
                if (string.IsNullOrEmpty(secim))
                {
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin!");
                    return;
                }
                switch (secim)
                {
                    case "1":
                        {
                            Console.WriteLine("Yazar eklemek için bilgileri giriniz.");
                            Console.Write("Yazar Adı: ");
                            string? name = Console.ReadLine();
                            Console.Write("Yazar Soyadı: ");
                            string? surname = Console.ReadLine();
                            Author author = new Author(0, name, surname);
                            int result = authorService.AddAuthor(author);
                            if (result > 0)
                            {
                                Console.WriteLine("Yazar başarıyla eklendi.");
                            }
                            else
                            {
                                Console.WriteLine("Yazar eklenemedi.");
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("YAZAR LİSTESİ:  ");
                            List<Author> authors = authorService.GetAllAuthors();
                            if (authors.Count > 0)
                            {
                                foreach (var item in authors)
                                {
                                    Console.WriteLine(item.AuthorID + " " + item.Name + " " + item.Surname);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Kayıtlı yazar bulunamadı.");
                            }
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Güncellemek istediğiniz yazarın ID'sini giriniz.");
                            int authorID = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Yazar Adı: ");
                            string? name = Console.ReadLine();
                            Console.Write("Yazar Soyadı: ");
                            string? surname = Console.ReadLine();
                            Author author = new Author(authorID, name, surname);
                            int result = authorService.UpdateAuthor(author);
                            if (result > 0)
                            {
                                Console.WriteLine("Yazar başarıyla güncellendi.");
                            }
                            else
                            {
                                Console.WriteLine("Yazar güncellenemedi.");
                            }
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Silmek istediğiniz yazarın ID'sini giriniz.");
                            int authorID = Convert.ToInt32(Console.ReadLine());
                            int result = authorService.DeleteAuthor(authorID);
                            if (result > 0)
                            {
                                Console.WriteLine("Yazar başarıyla silindi.");
                            }
                            else
                            {
                                Console.WriteLine("Yazar silinemedi.");
                            }
                            break;
                        }

                    case "5":
                        {
                            Console.WriteLine("Bilgilerini istediğiniz yazarın ID'sini giriniz.");
                            int authorID = Convert.ToInt32(Console.ReadLine());
                            Author author = authorService.GetAuthorById(authorID);
                            if (author.AuthorID != 0)
                            {
                                Console.WriteLine(author.AuthorID + " " + author.Name + " " + author.Surname);
                            }
                            else
                            {
                                Console.WriteLine("Yazar bulunamadı.");
                            }
                            break;
                        }
                    case "6":
                        {

                            Console.WriteLine("Bilgilerini görmek istediğiniz yazarın ismini yazınız: ");
                            string? name = Console.ReadLine();
                            List<Author> authors = authorService.GetAuthorsByName(name);
                            if (authors.Count > 0)
                            {
                                foreach (var item in authors)
                                {
                                    Console.WriteLine(item.AuthorID + " " + item.Name + " " + item.Surname);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Yazar bulunamadı.");
                            }
                            break;
                        }
                    case "7":
                        {
                            Console.WriteLine("Bilgilerini görmek istediğiniz yazarın adını ve soyadını yazınız: ");
                            Console.Write("Adı: ");
                            string? name = Console.ReadLine();
                            Console.Write("Soyadı: ");
                            string? surname = Console.ReadLine();
                            List<Author> authors = authorService.GetAuthorsByNameAndSurname(name, surname);
                            if (authors.Count > 0)
                            {
                                foreach (var item in authors)
                                {
                                    Console.WriteLine(item.AuthorID + " " + item.Name + " " + item.Surname);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Yazar bulunamadı.");
                            }
                            break;
                        }

                    case "8":
                        {
                            Console.WriteLine("Ana menüye dönülüyor...");
                            return;
                        }
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;










                }





            }




        }

        static void ShowLoansMenu(LoanService loanService)
        {

            while (true)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("---ÖDÜNÇ İŞLEMLERİ---");
                Console.WriteLine("1. Ödünç Ver");
                Console.WriteLine("2. Tüm Ödünçleri Listele");
                Console.WriteLine("3. Ödünç Güncelle");
                Console.WriteLine("4. Ödünç Silme");
                Console.WriteLine("5. Ödünç ID'sine göre ödünç bilgilerini getirme");
                Console.WriteLine("6. İadesi Geçmiş Ödünçleri Listele");
                Console.WriteLine("7. Belirtilen Tarih Aralığına Göre Ödünçleri Listele");
                Console.WriteLine("8. Ana Menüye Dön...");
                Console.Write("Seçiminizi yapın: ");

                string? secim = Console.ReadLine();
                if (string.IsNullOrEmpty(secim))
                {
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin!");
                    return;
                }
                switch (secim)
                {
                    case "1":
                        {
                            Console.WriteLine("Ödünç vermek için bilgileri giriniz.");
                            Console.WriteLine("Kitap ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int bookID))
                            {
                                Console.WriteLine("Geçersiz Kitap ID!");
                                return;
                            }

                            Console.WriteLine("Üye ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int memberID))
                            {
                                Console.WriteLine("Geçersiz Üye ID!");
                                return;
                            }

                            DateTime loanDate = DateTime.Now;
                            DateTime returnDate = loanDate.AddDays(14);

                            Loan newLoan = new Loan
                            {
                                BookID = bookID,
                                MemberID = memberID,
                                LoanDate = loanDate,
                                ReturnDate = returnDate
                            };

                            int result = loanService.AddLoan(newLoan);
                            if (result > 0)
                            {
                                Console.WriteLine("Ödünç başarıyla verildi.");
                            }
                            else
                            {
                                Console.WriteLine("Ödünç verme işlemi başarısız.");
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("ÖDÜNÇ LİSTESİ:  ");
                            List<Loan> loans = loanService.GetAllLoans();
                            if (loans.Count > 0)
                            {
                                foreach (var item in loans)
                                {
                                    Console.WriteLine(item.LoanID + " " + item.BookID + " " + item.MemberID + " " + item.LoanDate + " " + item.ReturnDate);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Kayıtlı ödünç bulunamadı.");
                            }
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Güncellemek istediğiniz ödünçün ID'sini giriniz.");
                            int loanID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Kitap ID: ");
                            int bookID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Üye ID: ");
                            int memberID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Ödünç verme tarihi: ");
                            DateTime loanDate = DateTime.Now;
                            Console.WriteLine("Ödünç iade tarihi: ");
                            DateTime returnDate = loanDate.AddDays(14);
                            Loan updatedLoan = new Loan
                            {
                                LoanID = loanID,
                                BookID = bookID,
                                MemberID = memberID,
                                LoanDate = loanDate,
                                ReturnDate = returnDate
                            };
                            int result = loanService.UpdateLoan(updatedLoan);
                            if (result > 0)
                            {
                                Console.WriteLine("Ödünç başarıyla güncellendi.");
                            }
                            else
                            {
                                Console.WriteLine("Ödünç güncellenemedi.");
                            }
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Silmek istediğiniz ödünçün ID'sini giriniz.");
                            int loanID = Convert.ToInt32(Console.ReadLine());
                            int result = loanService.DeleteLoan(loanID);
                            if (result > 0)
                            {
                                Console.WriteLine("Ödünç başarıyla silindi.");
                            }
                            else
                            {
                                Console.WriteLine("Ödünç silinemedi.");
                            }
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("Bilgilerini istediğiniz ödünçün ID'sini giriniz.");
                            int loanID = Convert.ToInt32(Console.ReadLine());
                            Loan loan = loanService.GetLoanById(loanID);
                            if (loan.LoanID != 0)
                            {
                                Console.WriteLine($"LoanID:{loan.LoanID}, BookID:{loan.BookID}, MemberID:{loan.MemberID}, LoanDate{loan.LoanDate}, ReturnDate{loan.ReturnDate}");
                            }
                            else
                            {
                                Console.WriteLine("Ödünç bulunamadı.");
                            }
                            break;
                        }

                    case "6":
                        {
                            Console.WriteLine("İadesi geçmiş ödünçleri listeleme işlemi: ");

                            List<OverdueBooks> overdueBooks = loanService.GetOverdueBooks();
                            if (overdueBooks.Count > 0)
                            {
                                foreach (var item in overdueBooks)
                                {
                                    Console.WriteLine(item.LoanID + " " + item.Title + " " + item.Name + " " + item.Surname + " " + item.LoanDate + " " + item.ReturnDate);
                                }
                            }
                            else
                            {
                                Console.WriteLine("İadesi geçmiş ödünç bulunamadı.");
                            }
                            break;
                        }
                    case "7":
                        {
                            Console.WriteLine("Belirtilen tarih aralığına göre ödünçleri listeleme işlemi: ");
                            Console.Write("Başlangıç Tarihi (yyyy-MM-dd): ");
                            string? input = Console.ReadLine();
                            if (string.IsNullOrEmpty(input))
                            {
                                Console.WriteLine("Geçersiz tarih girdisi!");
                                return;
                            }
                            DateTime startDate = DateTime.Parse(input);
                            Console.Write("Bitiş Tarihi (yyyy-MM-dd): ");
                            string? endDateInput = Console.ReadLine();
                            if (string.IsNullOrEmpty(endDateInput))
                            {
                                Console.WriteLine("Geçersiz tarih girdisi!");
                                return;
                            }
                            DateTime endDate = DateTime.Parse(endDateInput);
                            List<LoansByDate> loansByDates = loanService.GetLoansByDate(startDate, endDate);
                            if (loansByDates.Count > 0)
                            {
                                foreach (var item in loansByDates)
                                {
                                    Console.WriteLine(item.LoanID + " " + item.BookID + " " + item.Title + " " + item.MemberID + " " + item.LoanDate + " " + item.ReturnDate);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Belirtilen tarih aralığında ödünç bulunamadı.");
                            }
                            break;
                        }

                    case "8":
                        {
                            Console.WriteLine("Ana menüye dönülüyor...");
                            return;
                        }
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;





                }
            }
        }

        static void ShowMemberMenu(MemberService memberService)
        {

            while (true)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("---ÜYE İŞLEMLERİ---");
                Console.WriteLine("1. Üye Ekle");
                Console.WriteLine("2. Tüm Üyeleri Listele");
                Console.WriteLine("3. Üye Güncelle");
                Console.WriteLine("4. Üye Silme");
                Console.WriteLine("5. Üye ID'sine göre üye bilgilerini getir");
                Console.WriteLine("6. Üyelerİ ve ödünç aldıkları kitapları birlikte listeleme");
                Console.WriteLine("7. Ana Menüye Dön...");
                Console.Write("Seçiminizi yapın: ");

                string? secim = Console.ReadLine();
                if (string.IsNullOrEmpty(secim))
                {
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin!");
                    return;
                }
                switch (secim)
                {
                    case "1":
                        {
                            Console.WriteLine("Üye eklemek için bilgileri giriniz:  ");
                            Console.Write("Üye Adı: ");
                            string? name = Console.ReadLine();
                            Console.Write("Üye Soyadı: ");
                            string? surname = Console.ReadLine();
                            Console.Write("Üye E-posta: ");
                            string? email = Console.ReadLine();
                            Console.Write("Üye Telefon: ");
                            string? phone = Console.ReadLine();
                            Member newMember = new Member(0, name, surname, email, phone);
                            int result = memberService.AddMember(newMember);
                            if (result > 0)
                            {
                                Console.WriteLine("Üye başarıyla eklendi.");
                            }
                            else
                            {
                                Console.WriteLine("Üye eklenemedi.");
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("ÜYE LİSTESİ:  ");
                            List<Member> members = memberService.GetAllMembers();
                            if (members.Count > 0)
                            {
                                foreach (var item in members)
                                {
                                    Console.WriteLine(item.MemberID + " " + item.Name + " " + item.Surname + " " + item.Email + " " + item.Phone);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Kayıtlı üye bulunamadı.");
                            }
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Güncellemek istediğiniz üyenin ID'sini giriniz:  ");
                            int memberID = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Üye Adı: ");
                            string? name = Console.ReadLine();
                            Console.Write("Üye Soyadı: ");
                            string? surname = Console.ReadLine();
                            Console.Write("Üye E-posta: ");
                            string? email = Console.ReadLine();
                            Console.Write("Üye Telefon: ");
                            string? phone = Console.ReadLine();
                            Member updatedMember = new Member(memberID, name, surname, email, phone);
                            int result = memberService.UpdateMember(updatedMember);
                            if (result > 0)
                            {
                                Console.WriteLine("Üye başarıyla güncellendi.");
                            }
                            else
                            {
                                Console.WriteLine("Üye güncellenemedi.");
                            }
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Silmek istediğiniz üyenin ID'sini giriniz:  ");
                            int memberID = Convert.ToInt32(Console.ReadLine());
                            int result = memberService.DeleteMember(memberID);
                            if (result > 0)
                            {
                                Console.WriteLine("Üye başarıyla silindi.");
                            }
                            else
                            {
                                Console.WriteLine("Üye silinemedi.");
                            }
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("Bilgilerini istediğiniz üyenin ID'sini giriniz:  ");
                            int memberID = Convert.ToInt32(Console.ReadLine());
                            Member member = memberService.GetMemberById(memberID);
                            if (member.MemberID != 0)
                            {
                                Console.WriteLine(member.MemberID + " " + member.Name + " " + member.Surname + " " + member.Email + " " + member.Phone);
                            }
                            else
                            {
                                Console.WriteLine("Üye bulunamadı.");
                            }
                            break;
                        }
                    case "6":
                        {
                            Console.WriteLine("  ÜYELER VE ÖDÜNÇ  ALDIKLARI KİTAPLAR  ");
                            List<MembersWithLoans> membersWithLoans = memberService.GetMembersWithLoans();
                            if (membersWithLoans.Count > 0)
                            {
                                foreach (var item in membersWithLoans)
                                {
                                    Console.WriteLine(item.MemberID + " " + item.Name + " " + item.Surname + " " + item.Email + " " + item.Phone + " " + item.BookID + " " + item.LoanDate + " " + item.ReturnDate);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Üye bulunamadı.");
                            }
                            break;

                        }
                    case "7":
                        {
                            Console.WriteLine("Ana menüye dönülüyor...");
                            return;
                        }
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;




                }
            }


        }

    }

}

