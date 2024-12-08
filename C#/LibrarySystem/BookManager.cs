using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibrarySystem
{
    public class BookManager
    {
        // Hårdkodade böcker för testningssyften
        public void SeedBooks()
        {
            // Ansluter till databasen
            using (var context = new LibraryContext())
            {
                // Kontrollerar om det redan finns böcker
                if (!context.Books.Any())
                {   // Om det inte finns böcker läggs dessa till
                    context.Books.Add(new Book { Title = "Sagan om Ringen", Author = "J.R.R. Tolkien", ISBN = "9789172632189", AvailableCopies = 7 });
                    context.Books.Add(new Book { Title = "1984", Author = "George Orwell", ISBN = "9789173539678", AvailableCopies = 12 });
                    context.Books.Add(new Book { Title = "Harry Potter och De Vises Sten", Author = "J.K. Rowling", ISBN = "9789129723946", AvailableCopies = 5 });
                    context.Books.Add(new Book { Title = "Frankenstein", Author = "Mary Shelley", ISBN = "9789187193262", AvailableCopies = 10 });
                    // Sparar ändringarna till databasen
                    context.SaveChanges();
                }
            }
        }


        public void AddBook()
        {

            // Användarinput med validering så att det inte är tomt eller mellanslag
            Console.WriteLine("Ange titel:");
            string title = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Du måste ange en titel. Försök igen.");
                return;
            }

            Console.WriteLine("Ange författare:");
            string author = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Du måste ange en författare.. Försök igen.");
            }

            // Användarinput med validering som ser till att svaret är endast siffror och att det är 13 siffror långt
            Console.WriteLine("Ange ISBN");
            string isbn = Console.ReadLine()?.Trim();
            if (isbn.Length != 13 || !isbn.All(char.IsDigit))
            {
                Console.WriteLine("Ogiltigt ISBN. Det måste vara exakt 13 siffror. Försök igen.");
                return;
            }

            // Användarinput med validering som kontrollerar att svaret är större än noll
            Console.WriteLine("Ange antal tillgängliga exemplar:");
            if (!int.TryParse(Console.ReadLine(), out int availableCopies) || availableCopies < 1)
            {
                Console.WriteLine("Antalet böcker måste vara ett positivt heltal. Försök igen.");
                return;
            }

            // Ansluter till databasen
            using (var context = new LibraryContext())
            // Om bokens ISBN redan finns visas ett felmeddelande och användaren hänvisas till uppdateringsmetoden
            {
                if (context.Books.Any(b => b.ISBN == isbn))
                {
                    Console.WriteLine("En bok med samma ISBN finns redan. För att uppdatera boken måste du välja \"Uppdatera\" bok i menyn.");
                    return;
                }

                // Lagrar alla svar i variabler och tilldelar dessa till motsvarande kolumn i Book-tabellen
                var book = new Book
                {
                    Title = title,
                    Author = author,
                    ISBN = isbn,
                    AvailableCopies = availableCopies
                };
                context.Books.Add(book);
                context.SaveChanges();
                Console.WriteLine($"Boken '{title}' har lagts till. Det finns nu {availableCopies} exemplar.");

            }

        }

        public void ShowAllBooks()
        {   // Ansluter till databasen
            using (var context = new LibraryContext())
            {
                Console.WriteLine("Hämtar boklistan från databasen...");
                var books = context.Books.ToList();

                if (books.Any())
                {
                    Console.WriteLine("Boklista:");
                    Console.WriteLine("-----------------------------");
                    foreach (var book in books)
                    {
                        Console.WriteLine($"Titel: {book.Title}, Författare: {book.Author}, ISBN: {book.ISBN}, Tillgängliga exemplar: {book.AvailableCopies}");
                    }
                }
                else
                {
                    Console.WriteLine("Inga böcker hittades");
                }
            }
        }

        public void SearchBooks()
        {
            Console.Clear();
            Console.WriteLine("Sök efter böcker");
            Console.WriteLine("Välj vad du vill söka på:");
            Console.WriteLine("1. Titel");
            Console.WriteLine("2. Författare");
            Console.WriteLine("3. ISBN");
            Console.WriteLine("4. Återgå till huvudmenyn");

            int selection;
            if (int.TryParse(Console.ReadLine(), out selection))
            {
                string searchTerm = "";
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Ange titel att söka efter:");
                        searchTerm = Console.ReadLine()?.Trim();

                        break;

                    case 2:
                        Console.WriteLine("Ange författare att söka efter:");
                        searchTerm = Console.ReadLine()?.Trim();

                        break;

                    case 3:
                        Console.WriteLine("Ange ISBN att söka efter:");
                        searchTerm = Console.ReadLine()?.Trim();

                        break;

                    case 4:
                        Console.WriteLine("Återgå till användarmenyn");
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        return;
                }

                // Kontrollerar så att söktermen inte är tom

                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    Console.WriteLine("Du måste ange en sökterm.");
                    return;
                }

                // Hämta böcker baserat på sökterm, sökningen konverterar till lower för att säkerställa att rätt resultat ges
                using (var context = new LibraryContext())
                {
                    var books = context.Books.Where(b =>
                    b.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    b.Author.ToLower().Contains(searchTerm.ToLower()) ||
                    b.ISBN.ToLower().Contains(searchTerm.ToLower())
                    ).ToList();


                    if (books.Any())
                    {
                        Console.WriteLine("Vill du sortera resultaten?");
                        Console.WriteLine("1. Sortera efter titel");
                        Console.WriteLine("2. Sortera efter författare");
                        Console.WriteLine("3. Ingen sortering");

                        int sortSelection = int.Parse(Console.ReadLine());

                        switch (sortSelection)
                        {
                            case 1:
                                Console.WriteLine("Sökresultat, sorterat efter titel");
                                books = books.OrderBy(b => b.Title).ToList();
                                break;
                            case 2:
                                Console.WriteLine("Sökresultat, sorterat efter författare");
                                books = books.OrderBy(b => b.Author).ToList();
                                break;
                            case 3:
                                Console.WriteLine("Sökresultat, osorterat");

                                break;
                            default:
                                Console.WriteLine("Ogiltigt val, inga ändringar i sorteringen.");
                                break;

                        }
                        foreach (var book in books)
                        {
                            Console.WriteLine($"Titel: {book.Title} Författare: {book.Author} ISBN: {book.ISBN} Tillgängliga exemplar: {book.AvailableCopies}");
                        }
                        Console.WriteLine("Tryck på valfri tangent för att återgå till användarmenyn");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Inga böcker som matchar din sökning hittades.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt val, försök igen.");
            }
        }
        
    
        public void UpdateBook()
        {
            ShowAllBooks();
            // Samma felhantering av ISBN som ovan
            Console.WriteLine("Ange ISBN för den bok som ska redigeras:");
            string isbn = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("Ogiltigt ISBN. Det måste vara exakt 13 siffror. Försök igen.");
                return;
            }
            // Ansluter till databasen
            using (var context = new LibraryContext())
            {
                // Kontrollerar om boken finns
                var book = context.Books.FirstOrDefault(b => b.ISBN == isbn);
                if (book == null)
                {
                    Console.WriteLine("Boken hittades inte. Kontrollera ISBN och försök igen.");
                    return;
                }
                // Om den finns ombeds användaren mata in titel, författare och nytt antal böcker
                if (book != null)
                {
                    Console.WriteLine("Ange ny titel (lämna tomt för att behålla nuvarande titel)");
                    string newTitle = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrWhiteSpace(newTitle))
                    {
                        book.Title = newTitle;
                    }

                    Console.WriteLine("Ange ny författare (lämna tomt för att behålla nuvarande författare)");
                    string newAuthor = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrWhiteSpace(newAuthor))
                    {
                        book.Author = newAuthor;
                    }

                    Console.WriteLine("Ange nytt antal exemplar (lämna tomt för att behålla nuvarande antal)");
                    string newCount = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrWhiteSpace(newCount) && int.TryParse(newCount, out int updatedCount) && updatedCount >= 0)
                    {
                        book.AvailableCopies = updatedCount;
                    }

                    context.SaveChanges();
                    Console.WriteLine($"Boken '{book.Title}' har uppdaterats");
                }
            }
        }

        public void DeleteBook()
        {
            ShowAllBooks();
            Console.WriteLine("Ange ISBN för den bok du vill ta bort:");
            string isbn = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("Ogiltigt ISBN. Det måste vara exakt 13 siffror. Försök igen.");
                return;
            }
            // Ansluter till databasen
            using (var context = new LibraryContext())
            {
                // Kontrollerar om boken finns
                var book = context.Books.FirstOrDefault(b => b.ISBN == isbn);
                // Om den inte finns visas ett felmeddelande, annars raderas den
                if (book == null)
                {
                    Console.WriteLine("Boken hittades inte. Kontrollera ISBN och försök igen.");
                    return;
                }
                context.Books.Remove(book);
                context.SaveChanges();
                Console.WriteLine($"Boken '{book.Title}' har tagits bort");
                
            }
        }

        public void BorrowBook()
        {
            // Visar alla tillgängliga böcker
            ShowAllBooks();
            Console.WriteLine("Ange ISBN för boken du vill låna:");
            string isbn = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("Ogiltigt ISBN. Det måste vara exakt 13 siffror. Försök igen.");
                return;
            }
            // Ansluter till databasen
            using (var context = new LibraryContext())
            {
                var book = context.Books.FirstOrDefault(b => b.ISBN == isbn);
                if (book == null || book.AvailableCopies <= 0)
                {
                    Console.WriteLine("Boken är inte tillgänglig för utlåning.");
                    return;
                }
                
                Console.WriteLine("Ange ditt användarnamn:");
                string username = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace (username))
                {
                    Console.WriteLine("Användarnamn får inte vara tomt. Försök igen.");
                    return;
                }
                // Kontrollerar att boken finns och att det finns tillgängliga exemplar
                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    Console.WriteLine("Användaren hittades inte.");
                    return;
                }
                
                // Minska antalet exemplar och skapar ett nytt låneobjekt med dagens datum, och kopplar det till användaren
                book.AvailableCopies--;
                context.Loans.Add(new Loan
                {
                    UserId = user.Id,
                    ISBN = isbn,
                    LoanDate = DateTime.Now
                });
                context.SaveChanges();
                Console.WriteLine($"Boken '{book.Title}' har lånats av {username}.");
            }
        }

        public void LoanedBooks(string username)
        {
            // Hämtar aktiva lån för användare
            using (var context = new LibraryContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    // Hämtar alla aktiva lån genom att joina Loan och Book-tabellerna. Om ReturnDate är null räknas ett lån som aktivt
                    var loans = context.Loans
                        .Where(l => l.UserId == user.Id && l.ReturnDate == null)
                        .Join(context.Books,
                        loan => loan.ISBN,
                        book => book.ISBN,
                        (loan, book) => new { book.Title, book.Author, book.ISBN, loan.LoanDate })
                        .ToList();

                    // Om det finns aktiva lån skrivs dessa ut
                    if (loans.Any())
                    {
                        Console.WriteLine($"Låneböcker för {username}:");
                        Console.WriteLine("-----------------------------------------------------------------");
                        foreach (var loan in loans)
                        {
                            Console.WriteLine($"Titel: {loan.Title} Författare: {loan.Title} ISBN: {loan.ISBN} Lånedatum: {loan.LoanDate.ToShortDateString()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Du har inga aktiva lån.");
                    }
                }
                else
                {
                    Console.WriteLine("Användaren hittades inte.");
                }

            }
        }
        public void ReturnBook()
        {
            Console.WriteLine("Ange ditt användarnamn:");
            string username = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Användarnamn får inte vara tomt. Försök igen.");
                return;
            }

            // Hämtar och visar användarens aktiva lån
            LoanedBooks(username);

            Console.WriteLine("Ange ISBN för boken du vill återlämna:");
            string isbn = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("Ogiltigt ISBN. Det måste vara exakt 13 siffror. Försök igen.");
                return;
            }

            using (var context = new LibraryContext())
            {   // Hämtar användaren från databasen och kontrollerar att den finns
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                // Om användaren inte är null så hämtas användarens lån från databasen
                if (user != null)
                {   
                    // Hämtar information om den specifika boken och användaren
                    var loan = context.Loans.FirstOrDefault(l => l.ISBN == isbn && l.UserId == user.Id && l.ReturnDate == null);
                    // Om användarens lån inte är null registreras återlämningsdatum, och en till kopia läggs till ii tillgängliga böcker
                    if (loan != null)
                    {
                       
                        loan.ReturnDate = DateTime.Now;

                        var book = context.Books.FirstOrDefault(b => b.ISBN == isbn);
                        if (book != null)
                        {
                            book.AvailableCopies++;
                        }

                        context.SaveChanges();
                        Console.WriteLine($"Boken '{book.Title}' har återlämnats av {username}.");
                    }
                    else
                    {
                        Console.WriteLine("Inget aktivt lån hittades för denna bok.");
                    }
                }
                else
                {
                    Console.WriteLine("Användern hittades inte.");
                }
            }

        }
    }
        
}
