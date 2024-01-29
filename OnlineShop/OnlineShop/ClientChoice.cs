using System;
using System.Xml;

namespace OnlineShop
{
    internal class ClientChoice
    {
        //Ścieżka do pliku zawierającego produkty
        private const string productsFilePath = "Data/Products.csv";

        //Metoda do przeglądania dostępnych produktów
        public void BrowseProducts()
        {
            Console.Clear();

            Console.WriteLine("Przeglądanie produktów:");
            string[] allProducts = File.ReadAllLines(productsFilePath);//Odczytanie wszystkich produktów z pliku
            Console.WriteLine("Lista dostępnych produktów:");
            foreach (var product in allProducts)//Wyświetlenie listy dostępnych produktów
            {
                string[] productDetails = product.Split(',');
                Console.WriteLine($"ID: {productDetails[0]}, Nazwa: {productDetails[1]}, Cena: {productDetails[5]}, Dostępność: {productDetails[4]}");
            }

            //Pętla nieskończona, pozwalająca na powrót do menu
            while (true)
            {
                Console.WriteLine("Napisz 'exit' aby wrócić do menu: ");
                string exit = Console.ReadLine();
                if (exit == "exit")
                {
                    Console.Clear();
                    new LoginResult().CustomerOptions();
                }
            }
        }

        //Metoda do zakupu produktu
        public void BuyProduct()
        {
            Console.Clear();

            Console.WriteLine("Kupowanie produktu:");

            //Pobranie ID produktu do zakupu
            Console.Write("Podaj ID produktu, który chcesz kupić: ");
            int productId = int.Parse(Console.ReadLine());

            //Pobranie ilości produktu do zakupu
            Console.Write("Podaj ilość produktu, którą chcesz kupić: ");
            int quantity = int.Parse(Console.ReadLine());

            //Odczytanie wszystkich produktów z pliku
            string[] allProducts = File.ReadAllLines(productsFilePath);

            //Sprawdzenie czy produkt o podanym ID istnieje
            string product = allProducts.FirstOrDefault(p => p.Split(',')[0] == productId.ToString());

            if (product == null)
            {
                Console.Clear();
                Console.WriteLine("Produkt o podanym ID nie istnieje.");
                new LoginResult().CustomerOptions();
            }

            //Sprawdzenie dostępności produktu
            string[] productDetails = product.Split(',');
            int availableQuantity = int.Parse(productDetails[5]);

            if (quantity > availableQuantity)
            {
                Console.Clear();
                Console.WriteLine("Nie wystarczająca ilość produktu na stanie.");
                new LoginResult().CustomerOptions();
            }

            //Aktualizacja stanu produktu
            int updatedQuantity = availableQuantity - quantity;
            productDetails[4] = updatedQuantity.ToString();

            //Aktualizacja pliku z produktami
            string updatedProduct = string.Join(",", productDetails);
            int index = Array.IndexOf(allProducts, product);
            allProducts[index] = updatedProduct;
            File.WriteAllLines(productsFilePath, allProducts);

            Console.Clear();
            Console.WriteLine("Produkt został pomyślnie zakupiony.");
            new LoginResult().CustomerOptions();
        }

        //Metoda do sprzedaży produktu
        public void SellProduct()
        {
            Console.Clear();
            
            Console.WriteLine("Sprzedaj produkt:");

            //Pobranie nazwy produktu
            Console.Write("Podaj nazwę produktu: ");
            string productName = Console.ReadLine();

            //Pobranie ceny produktu
            Console.Write("Podaj cenę produktu: ");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price) || price <= 0)
            {
                Console.WriteLine("Nieprawidłowa cena. Podaj poprawną wartość.");
            }

            //Pobranie ilości produktu
            Console.Write("Podaj ilość produktu: ");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
            {
                Console.WriteLine("Nieprawidłowa ilość. Podaj poprawną wartość.");
            }

            //Pobierz nazwy producenta
            Console.Write("Podaj nazwę producenta: ");
            string manufacturerName = Console.ReadLine();

            int manufacturerId = new GetOrCreate().GetOrCreateManufacturer(manufacturerName);

            //Pobranie kategorii
            Console.Write("Podaj nazwę kategorii: ");
            string categoryName = Console.ReadLine();

            int categoryId = new GetOrCreate().GetOrCreateCategory(categoryName);

            //Dodanie nowego produktu do pliku CSV
            int nextProductId = new GetOrCreate().GetNextProductId();
            string newProduct = $"{nextProductId},{productName},{manufacturerId},{categoryId},{quantity},{price}";
            File.AppendAllLines(productsFilePath, new[] { newProduct });

            Console.Clear();
            Console.WriteLine("Produkt został dodany pomyślnie.");
            new LoginResult().CustomerOptions();
        }
    }
}
