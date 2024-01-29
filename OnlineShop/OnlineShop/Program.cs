namespace OnlineShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w aplikacji sklepu odzieżowego!");
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
                    //rejestracja
                    Console.Clear();
                    NewUser.RegisterUser();
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
    }
}