using System;

namespace OnlineShop
{
    internal class Products
    {
        private const string productsFilePath = "Data/Products.csv";

        //Metoda do dodawania nowego produktu
        public static void AddProduct()
        {
            Console.WriteLine("Dodawanie nowego produktu:");

            //Pobieranie nazwy produktu
            Console.Write("Podaj nazwę produktu: ");
            string name = Console.ReadLine();

            //Pobieranie ID producenta
            Console.Write("Podaj ID producenta: ");
            string manufacturerID = Console.ReadLine();

            //Pobieranie ID kategorii
            Console.Write("Podaj ID kategorii: ");
            string categoryID = Console.ReadLine();

            //Pobieranie rozmiaru
            Console.Write("Podaj rozmiar (S/M/L): ");
            string size = Console.ReadLine();

            //Pobieranie ilości
            Console.Write("Podaj ilość: ");
            string quantity = Console.ReadLine();

            //Pobieranie ceny
            Console.Write("Podaj cenę: ");
            string price = Console.ReadLine();

            //Tworzenie nowego wiersza dla nowego produktu w pliku CSV
            string newProductRecord = $"{name},{manufacturerID},{categoryID},{size},{quantity},{price}";

            //Dodawanie nowego produktu do pliku CSV
            AddRecordToCsv(productsFilePath, newProductRecord);

            Console.WriteLine("Nowy produkt został dodany.");
        }

        //Metoda prywatna do dodawania nowego rekordu do pliku CSV
        private static void AddRecordToCsv(string filePath, string newRecord)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(newRecord);
            }
        }
    }
}
