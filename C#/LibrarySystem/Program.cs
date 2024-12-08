using LibrarySystem;
using System.Data.Entity.Migrations.Model;

var bookManager = new BookManager(); 
var userManager = new UserManager();

bookManager.SeedBooks();
userManager.SeedUsers();
// Huvudloop, while true så körs programmete
while (true)
{
    Console.Clear();
    Console.WriteLine("Välkommen till biblioteket! Hur kan vi hjälpa dig idag?");
    Console.WriteLine("--------------------------------------------------------");
    Console.WriteLine("Välj en åtgärd:");
    Console.WriteLine("1. Registrering");
    Console.WriteLine("2. Inloggning");
    Console.WriteLine("3. Avsluta program");

    // Användarinput av val
    int selection;
    if (int.TryParse(Console.ReadLine(), out selection))
    {
        Console.Clear();

        switch(selection)
        {
            case 1:
                Console.WriteLine("Registrera dig");
                userManager.AddUser();
                Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn");
                Console.ReadKey();
                break;

            case 2:
                Console.WriteLine("Logga in");
                Login();
                Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn");
                Console.ReadKey();
                break;

            case 3:
                Console.WriteLine("Avsluta");
                return;

            default:
                Console.WriteLine("Felaktigt val, försök igen.");
                break;

        }
            
    }

}


void Login()
{
    // Tar emot användarens inloggningsuppgifter och validerar dem
    Console.WriteLine("Ange ditt användarnamn:");
    string username = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(username))
    {
        Console.WriteLine("Användarnamn får inte vara tomt. Försök igen.");
        return;
    }

    Console.WriteLine("Ange ditt lösenord:");
    string password = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("Lösenord får inte vara tomt. Försök igen.");
    }
    // Ansluter till databasen
    using (var context = new LibraryContext())
    {
        // Hämtar användaren och jämför användarnamn och lösenord mot databasen
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user != null && user.Password == password)
        {
            // Om användarens roll är admin så visas adminmenyn, om inte visas användarmenyn
            Console.WriteLine($"Välkommen {username}!");
            if (user.Admin)
            {
                AdminMenu(bookManager, userManager);
            }
            else
            {
                UserMenu(bookManager);
            }
        }
        else
        {
            Console.WriteLine("Fel användarnamn eller lösenord. Försök igen.");

        }
    }
}

void UserMenu(BookManager bookmanager)
{ // Samma struktur som huvudmenyn
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Användarmeny");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("1. Boklista");
        Console.WriteLine("2. Sök i boklistan");
        Console.WriteLine("3. Låna en bok");
        Console.WriteLine("4. Återlämna en bok");
        Console.WriteLine("5. Återgå till huvudmenyn");
        Console.WriteLine("-------------------------------");

        int selection;
        if (int.TryParse(Console.ReadLine(), out selection))
        {
            Console.Clear();

            switch (selection)
            {
                case 1:
                    Console.WriteLine("Boklista");
                    bookmanager.ShowAllBooks();
                    Console.WriteLine("Tryck på valfri tangent för att återgå till användarmenyn");
                    Console.ReadKey();
                    break;

                case 2: 
                    Console.WriteLine("Sök efter bok");
                    bookManager.SearchBooks();
                    Console.WriteLine("Tryck på valfri tangent för att återgå till användarmenyn");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Låna en bok");
                    bookmanager.BorrowBook();
                    Console.WriteLine("Tryck på valfri tangent för att återgå till användarmenyn");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.WriteLine("Återlämna en bok");
                    bookmanager.ReturnBook();
                    Console.WriteLine("Tryck på valfri tangent för att återgå till användarmenyn");
                    Console.ReadKey();
                    break;

                case 5:
                    Console.WriteLine("Återgå till huvudmenyn");
                    return;

                default:
                    Console.WriteLine("Felaktigt val, försök igen.");
                    break;
            }
        }
    }

}

void AdminMenu(BookManager bookmanager, UserManager usermanager)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Administratörsmeny");
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("1. Lägg till bok");
        Console.WriteLine("2. Ta bort bok");
        Console.WriteLine("3. Uppdatera bokinformation");
        Console.WriteLine("4. Visa boklista");
        Console.WriteLine("5. Visa användarinformation"); 
        Console.WriteLine("6. Återgå till huvudmenyn");
        Console.WriteLine("-------------------------------------------------");

        int selection;
        if (int.TryParse(Console.ReadLine(), out selection))
        {
            Console.Clear();

            switch (selection)
            {
                case 1:
                    Console.WriteLine("Lägg till en ny bok");
                    bookmanager.AddBook();
                    Console.WriteLine("Tryck på valfri tangent för att återgå till administratörsmenyn");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine("Ta bort en bok");
                    bookmanager.DeleteBook();
                    Console.WriteLine("Tryck på valfri tangent för att återgå till administratörsmenyn");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Uppdatera bokinformation");
                    bookmanager.UpdateBook();
                    Console.WriteLine("Tryck på valfri tangent för att återgå till administratörsmenyn");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.WriteLine("Boklista");
                    bookmanager.ShowAllBooks();
                    Console.WriteLine("Tryck på valfri tangent för att återgå till administratörsmenyn");
                    Console.ReadKey();
                    break;

                case 5:
                    Console.WriteLine("Användarlista");
                    usermanager.ShowAllUsers();
                    Console.WriteLine("Tryck på valfri tangent för att återgå till administratörsmenyn");
                    Console.ReadKey();
                    break;

                case 6:
                    Console.WriteLine("Återgå till huvudmenyn");
                    return;

                default:
                    Console.WriteLine("Felaktigt val, försök igen.");
                    break;
            }

        }
    }

}

