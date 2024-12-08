using sqlLab;
using System.Data.Entity.Migrations.Model;

// Metod för att befolka "seeda" databasen mede ett par användare
SeedDatabase();

// Huvudloop håller igång programmet tills användaren väljer att avsluta
while (true)
{
    Console.WriteLine("Välkommen!");
    Console.WriteLine("---------------------------");
    Console.WriteLine("Välj en åtgärd:");
    Console.WriteLine("1. Registrering");
    Console.WriteLine("2. Inloggning");
    Console.WriteLine("3. Visa användare");
    Console.WriteLine("4. Uppdatera användare");
    Console.WriteLine("5. Radera användare");
    Console.WriteLine("6. Avsluta");
    Console.WriteLine("---------------------------");

    // Tar emot användarinput för att välja case
    int selection;
    if (int.TryParse(Console.ReadLine(), out selection))
    {
        Console.Clear();

        switch (selection)
        {
            case 1:
                Console.WriteLine("Registrering");
                AddUser();
                Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn.");
                Console.ReadKey();
                break;

            case 2:
                Console.WriteLine("Inloggning");
                Login();
                Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn.");
                Console.ReadKey();
                break;

            case 3:
                Console.WriteLine("Visa användare");
                ShowUser();
                Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn.");
                Console.ReadKey();
                break;

            case 4:
                Console.WriteLine("Byt lösenord");
                UpdateUser();
                Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn.");
                Console.ReadKey();
                break;

            case 5:
                Console.WriteLine("Radera användare");
                DeleteUser();
                Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn.");
                Console.ReadKey();
                break;

            case 6:
                Console.WriteLine("Avsluta");
                return;

            default:
                Console.WriteLine("Felaktigt val, försök igen.");
                Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn.");
                Console.ReadKey();
                break;
        
        }
        Console.Clear();
    }

}

// Valde att skapa en metod även för login för att göra huvudloopen mer lättläst
bool Login()
{
    Console.WriteLine("Ange ditt användarnamn: ");
    string username = Console.ReadLine();

    Console.WriteLine("Ange ditt lösenord: ");
    string password = Console.ReadLine();

    // Kontrollerar användarnamn och lösenord mot databasen. Returnerar true om inloggningen lyckas, annars false.
    var user = GetUser(username);
    if (user != null && user.Password == password)
    {
        Console.WriteLine("Du är inloggad!");
        return true;
    }
    else
    {
        Console.WriteLine("Fel användarnamn eller lösenord.");
        return false;
    }
}

void DeleteUser()
{
    Console.WriteLine("Ange användarnamnet gör användaren du vill ta bort:");
    string username = Console.ReadLine();

    // Skapar en databasanslutning som automatiskt stängs när blocket avslutas
    using (var context = new UserContext())
    {
        // Hämtar den användare som ska raderas, och OM användaren finns raderas den, annars visas ett felmeddelande
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            context.Users.Remove(user);
            context.SaveChanges();
            Console.WriteLine($"Användaren {username} har tagits bort.");
        }
        else
        {
            Console.WriteLine("Användaren hittades inte.");
        }
    }
}

void UpdateUser()
{
    Console.WriteLine("Ange användarnamnet för användaren du vill uppdatera: ");
    string username = Console.ReadLine();

    // Skapar en databasanslutning som automatiskt stängs när blocket avslutas
    using (var context = new UserContext())
    {
        // Hämtar användarnamnt och om det finns i listan tas användarinput emot för ändring av lösenord. Sedan sparas det nya lösenordet i databasen.
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            Console.WriteLine("Ange nytt lösenord:");
            string newPassword = Console.ReadLine();

            user.Password = newPassword;
            context.SaveChanges();

            Console.WriteLine($"Användaren {username} har uppdaterats");
        }
        else
        {
            Console.WriteLine("Användaren hittades inte.");
        }
    }
}

void ShowUser()
{
    // Skapar en databasanslutning som automatiskt stängs när blocket avslutas
    using (var context = new UserContext())
    {
        var users = context.Users.ToList();

        // Om det finns användare så körs en foreach-loop och visar varje användarnamn och lösenord i listan. I normala fall bör man naturligtvis inte visa sina lösenord i en lista så här, men för att det ska vara enkelt med testningen så syns dessa i denna metod.
        if (users.Any())
        {
            Console.WriteLine("Lista över alla användare:");
            foreach (var user in users)
            {
                Console.WriteLine($"Användarnamn: {user.Username}, Lösenord: {user.Password}");
            }
        }
        else
        {
            Console.WriteLine("Inga användare hittades.");
        }
    }

}

void AddUser()
{
    Console.WriteLine("Ange ett användarnamn: ");
    string newUsername = Console.ReadLine();

    Console.WriteLine("Ange ett lösenord: ");
    string newPassword = Console.ReadLine();

    // Skapar databasanslutning som automatiskt stängs när blocket avslutas
    using (var context = new UserContext())
    {
        var user = new User
        {
            Username = newUsername,
            Password = newPassword
        };

        context.Users.Add(user);
        context.SaveChanges();
    }
    Console.WriteLine("Konto skapat!");
}

User GetUser(string username )
{
    // Skapar en databasanslutning som automatiskt stängs när blocket avslutats
    using (var context = new UserContext())
    {
        return context.Users.FirstOrDefault(u => u.Username == username);
    }
}

// Skapar en funktion för hårdkodade användare. Denna körs vid start och förhindrar
// att dubletter av användare registreras
void SeedDatabase()
{
   using(var context = new UserContext())
    {
        if (!context.Users.Any())
        {
            context.Users.Add(new User { Username = "anv1", Password = "pass1" });
            context.Users.Add(new User { Username = "anv2", Password = "pass2" });
            context.Users.Add(new User { Username = "anv3", Password = "pass3" });
            context.SaveChanges();
        }
    }
}