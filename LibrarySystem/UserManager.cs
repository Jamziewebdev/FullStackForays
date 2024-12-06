using LibrarySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibrarySystem
{
    public class UserManager
    {
        public void SeedUsers()
        {
            // Seedar tabellen för users på samma sätt som förklarat i BookManager
            using (var context = new LibraryContext())
            {
                if (!context.Users.Any())
                {
                    context.Users.Add(new User { Username = "Admin", Password = "123", Admin = true });
                    context.Users.Add(new User { Username = "Test1", Password = "234", Admin = false });
                    context.Users.Add(new User { Username = "Test2", Password = "345", Admin = false });
                    context.Users.Add(new User { Username = "Test3", Password = "456", Admin = false });
                    context.SaveChanges();
                }
            }
        }


        public void AddUser()
        {
            
            // Samlar in användarnamn, lösenord och roll från användaren och kontrollerar att input är det jag vill att den ska vara
            Console.WriteLine("Ange ett användarnamn:");
            string newUsername = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(newUsername))
            {
                Console.WriteLine("Användarnamn får inte vara tomt. Försök igen.");
                return;
            }

            Console.WriteLine("Ange ett lösenord:");
            string newPassword = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                Console.WriteLine("Lösenord får inte vara tomt. Försök igen.");
            }

            Console.WriteLine("Är detta ett administratörskonto? Svara 'Y' för Ja eller 'N' för Nej:");
            string roleInput = Console.ReadLine()?.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(roleInput) || roleInput != "Y" && roleInput != "N")
            {
                Console.WriteLine("Du måste svara 'Y' eller 'N'.");
            }

            bool isAdmin = roleInput == "Y";

            // Om allt är korrekt inmatat skapas en ny databasanslutning och variablerna för inmatning paras ihop med motsvarande del i tabellen
            using (var context = new LibraryContext())
            {
                var user = new User
                {
                    Username = newUsername,
                    Password = newPassword,
                    Admin = isAdmin
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
            Console.WriteLine($"Konto för {newUsername} har skapats som {(isAdmin ? "administratör" : "användare")}.");
        }


        public void ShowAllUsers()
        {
            // Skapar databasanslutning och omvandlar innehållet i Users-tabellen till en lista
            using (var context = new LibraryContext())
            {
                var users = context.Users.ToList();

                // Om det finns användare i tabellen skrivs de ut i detta format:
                if (users.Any())
                {
                    Console.WriteLine("Användarlista:");
                    Console.WriteLine("-----------------------------");
                    foreach (var user in users)
                    {
                        int loanCount = context.Loans.Count(l => l.UserId == user.Id);
                        Console.WriteLine($"Användarnamn: {user.Username}, Roll: {(user.Admin ? "Administratör" : "Användare")}, Antal l{loanCount}");
                    }
                }
                else
                {
                    Console.WriteLine("Inga användare hittades.");
                }
            }
        }
    }
}
