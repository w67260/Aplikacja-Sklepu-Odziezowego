using System;

namespace OnlineShop
{
    internal class LoginResult
    {
        public void ShowOptions(int loginResult)
        {
            switch (loginResult)
            {
                case 1:
                    //Opcje dla administratora
                    AdminOptions();
                    break;
                case 2:
                    //Opcje dla klienta
                    CustomerOptions();
                    break;
                default:
                    Console.WriteLine("Spróbuj ponownie");
                    break;
            }
        }

        //Metoda wyświetlająca opcje dla administratora
        public void AdminOptions()
        {
            Console.WriteLine("1. Dodaj produkt");
            Console.WriteLine("2. Usuń produkt");
            Console.WriteLine("3. Edytuj produkt");
            Console.WriteLine("4. Dodaj kategorię");
            Console.WriteLine("5. Usuń kategorię");
            Console.WriteLine("6. Edytuj kategorię");
            Console.WriteLine("7. Dodaj producenta");
            Console.WriteLine("8. Usuń producenta");
            Console.WriteLine("9. Edytuj producenta");
            Console.WriteLine("10. Edytuj użytkownika");
            Console.WriteLine("11. Usuń użytkownika");
            Console.WriteLine("12. Wyjdz z aplikacji");
            Console.Write("Wybierz opcje: ");
            string choice = Console.ReadLine();

            //Wybór opcji przez administratora
            switch (choice)
            {
                case "1":
                    new AdminChoice().AddProduct();
                    break;
                case "2":
                    new AdminChoice().RemoveProduct();
                    break;
                case "3":
                    new AdminChoice().EditProduct();
                    break;
                case "4":
                    new AdminChoice().AddCategory();
                    break;
                case "5":
                    new AdminChoice().RemoveCategory();
                    break;
                case "6":
                    new AdminChoice().EditCategory();
                    break;
                case "7":
                    new AdminChoice().AddManufacturer();
                    break;
                case "8":
                    new AdminChoice().RemoveManufacturer();
                    break;
                case "9":
                    new AdminChoice().EditManufacturer();
                    break;
                case "10":
                    new AdminChoice().EditUser();
                    break;
                case "11":
                    new AdminChoice().RemoveUser();
                    break;
                case "12":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                    break;
            }
        }

        //Metoda wyświetlająca opcje dla klienta
        public void CustomerOptions()
        {
            Console.WriteLine("1. Przeglądaj produkty");
            Console.WriteLine("2. Kup produkt");
            Console.WriteLine("3. Wystaw produkt na sprzedaż");
            Console.WriteLine("4. Wyjdz z aplikacji");
            Console.WriteLine("Wybierz opcję: ");
            string choice = Console.ReadLine();

            //Wybór opcji przez klienta
            switch (choice)
            {
                case "1":
                    new ClientChoice().BrowseProducts();
                    break;
                case "2":
                    new ClientChoice().BuyProduct();
                    break;
                case "3":
                    new ClientChoice().SellProduct();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                    break;
            }
        }
    }
}
