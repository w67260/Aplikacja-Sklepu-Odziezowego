using System;

namespace OnlineShop
{
    internal class NewUser
    {
        private const string usersFilePath = "Data/Users.csv";

        public static void RegisterUser()
        {
            Console.Clear();

            Console.WriteLine("Dodawanie nowego użytkownika:");

            //Pobranie nazwy użytkownika
            Console.Write("Podaj nazwę użytkownika: ");
            string username = Console.ReadLine();

            //Pobranie hasła
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine();

            //Pobranie miasta
            Console.Write("Podaj miasto: ");
            string city = Console.ReadLine();

            //Pobranie adresu email
            Console.Write("Podaj adres email: ");
            string email = Console.ReadLine();

            //Domyślnie nowy użytkownik będzie zarejestrowany jako klient
            string role = "Klient";

            //Tworzymy nowy wiersz dla nowego użytkownika w pliku CSV
            int nextUserID = GetOrCreate.GetNextUserID();
            string newUserRecord = $"{nextUserID},{username},{password},{city},{email},{role}";

            // Dodajemy nowego użytkownika do pliku CSV
            AddUserToCsv(newUserRecord);

            Console.Clear();
            Console.WriteLine("Nowy użytkownik został dodany.");

            //Opcje dla nowo utworzonego klienta
            Console.WriteLine("Wybierz akcję:");
            Console.WriteLine("1. Zaloguj się");
            Console.WriteLine("2. Zarejestruj nowego użytkownika");
            Console.WriteLine("3. Wyjdź");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    //Logowanie
                    Console.Clear();
                    Login.PerformLogin();
                    break;
                case "2":
                    //Rejestracja kolejnego użytkownika
                    Console.Clear();
                    RegisterUser();
                    break;
                case "3":
                    //Zamknięcie aplikacji
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                    break;
            }
        }

        private static void AddUserToCsv(string newUserRecord)
        {
            try
            {
                //Sprawdzamy, czy plik już istnieje
                bool fileExists = File.Exists(usersFilePath);

                using (StreamWriter writer = new StreamWriter(usersFilePath, true))
                {
                    //Dodajemy nowy wiersz
                    if (fileExists)
                    {
                        writer.WriteLine();
                    }
                    writer.Write(newUserRecord);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas dodawania użytkownika: " + ex.Message);
            }
        }
    }
}
