using System;

namespace OnlineShop
{
    internal class Login
    {
        //Ścieżka do pliku przechowującego dane użytkowników
        private const string usersFilePath = "Data/Users.csv";

        //Metoda do wykonywania logowania
        public static void PerformLogin()
        {
            Console.Clear();

            Console.WriteLine("Logowanie:");

            //Pobranie nazwy uzytkownika
            Console.Write("Podaj nazwę użytkownika: ");
            string username = Console.ReadLine();

            //Pobranie hasła
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine();

            Console.Clear();

            //Walidacja użytkownika
            string role = ValidateUser(username, password);

            //Obsługa różnych ról użytkowników
            if (role == "Admin")//Admin
            {
                Console.WriteLine("Zalogowano jako administrator.");
                new LoginResult().ShowOptions(1);
            }
            else if (role == "Klient")//Klinet
            {
                Console.WriteLine("Zalogowano jako klient.");
                new LoginResult().ShowOptions(2);
            }
            else//Błędne dane logowania - powrót do menu głównego
            {
                Console.WriteLine("Nieprawidłowa nazwa użytkownika lub hasło.");

                Console.WriteLine("Wybierz akcję:");
                Console.WriteLine("1. Zaloguj się");
                Console.WriteLine("2. Zarejestruj nowego użytkownika");
                Console.WriteLine("3. Wyjdź");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        PerformLogin();
                        break;
                    case "2":
                        Console.Clear();
                        NewUser.RegisterUser();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                        break;
                }
            }
        }

        //Metoda do walidacji użytkownika
        private static string ValidateUser(string username, string password)
        {
            //Odczyt linii z pliku zawierającego dane użytkowników
            string[] lines = File.ReadAllLines(usersFilePath);

            //Iteracja przez linie danych użytkowników
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');

                if (parts[1] == username && parts[2] == password)
                {
                    return parts[5]; // Zwrócenie roli użytkownika
                }
            }

            return null;//Zwrócenie braku roli (niepowodzenie walidacji)
        }
    }
}
