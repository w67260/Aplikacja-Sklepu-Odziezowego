using System;

namespace OnlineShop
{
    internal class AdminChoice
    {
        //Ścieżka do pliku zawierającego produkty
        private const string productsFilePath = "Data/Products.csv";
        //Ścieżka do pliku zawierającego kategorie
        private const string categoriesFilePath = "Data/Categories.csv";
        //Ścieżka do pliku zawierającego producentów
        private const string manufacturersFilePath = "Data/Manufacturers.csv";
        //Ścieżka do pliku zawierającego użytkowników
        private const string usersFilePath = "Data/Users.csv";

        //Metoda do dodawania nowego produktu
        public void AddProduct()
        {
            Console.Clear();

            Console.WriteLine("Dodawanie nowego produktu:");

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

            //Pobranie nazwy producenta
            Console.Write("Podaj nazwę producenta: ");
            string manufacturerName = Console.ReadLine();

            int manufacturerId = new GetOrCreate().GetOrCreateManufacturer(manufacturerName);

            //Pobranie kategorii
            Console.Write("Podaj nazwę kategorii: ");
            string categoryName = Console.ReadLine();

            int categoryId = new GetOrCreate().GetOrCreateCategory(categoryName);

            //Pobranie kolejnego ID produktu
            int nextProductId = new GetOrCreate().GetNextProductId();
            string newProduct = $"{nextProductId},{productName},{manufacturerId},{categoryId},{quantity},{price}";

            //Dodanie nowego produktu do pliku
            File.AppendAllLines(productsFilePath, new[] { newProduct });

            Console.Clear();
            Console.WriteLine("Produkt został dodany pomyślnie.");
            //Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }

        //Metoda do usuwania produktu
        public void RemoveProduct()
        {
            Console.Clear();
            
            Console.WriteLine("Usuwanie produktu:");

            //Pobranie ID produktu do usunięcia
            Console.Write("Podaj ID produktu do usunięcia: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                //Odczytanie wszystkich produktów z pliku
                string[] allProducts = File.ReadAllLines(productsFilePath);

                if (allProducts.Any(p => p.Split(',')[0] == productId.ToString()))
                {
                    //Usunięcie produktu z bazy
                    string[] updatedProducts = allProducts.Where(p => p.Split(',')[0] != productId.ToString()).ToArray();
                    File.WriteAllLines(productsFilePath, updatedProducts);
                    Console.Clear();
                    Console.WriteLine("Produkt został usunięty pomyślnie.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Produkt o podanym ID nie istnieje.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nieprawidłowy format ID produktu.");
            }
            // Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }

        //Metoda do edycji konkretnego produktu
        public void EditProduct()
        {
            Console.Clear();
            Console.WriteLine("Edycja produktu:");

            //Pobranie ID produktu do edycji
            Console.Write("Podaj ID produktu do edycji: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                //Odczytanie wszystkich produktów z pliku
                string[] allProducts = File.ReadAllLines(productsFilePath);

                //Sprawdzenie, czy produkt istnieje
                string product = allProducts.FirstOrDefault(p => p.Split(',')[0] == productId.ToString());
                if (product != null)
                {
                    //Pobranie nazwy produktu
                    Console.Write("Nowa nazwa produktu: ");
                    string newProductName = Console.ReadLine();

                    //Pobranie nowej ceny produktu
                    Console.Write("Nowa cena produktu: ");
                    double newPrice;
                    while (!double.TryParse(Console.ReadLine(), out newPrice) || newPrice <= 0)
                    {
                        Console.WriteLine("Nieprawidłowa cena. Podaj poprawną wartość.");
                    }

                    //Pobranie nowej ilości
                    Console.Write("Nowa ilość produktu: ");
                    int newQuantity;
                    while (!int.TryParse(Console.ReadLine(), out newQuantity) || newQuantity < 0)
                    {
                        Console.WriteLine("Nieprawidłowa ilość. Podaj poprawną wartość.");
                    }

                    //Znalezienie indeku produktu w tablicy
                    int index = Array.FindIndex(allProducts, p => p.Split(',')[0] == productId.ToString());

                    //Zaktualizowanie danych produktu
                    allProducts[index] = $"{productId},{newProductName},{product.Split(',')[2]},{product.Split(',')[3]},{newQuantity},{newPrice}";

                    //Zapisanie zmienionej tablicy do pliku
                    File.WriteAllLines(productsFilePath, allProducts);

                    Console.WriteLine("Produkt został zaktualizowany pomyślnie.");
                }
                else
                {
                    Console.WriteLine("Produkt o podanym ID nie istnieje.");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy format ID produktu.");
            }
            Console.Clear();
            // Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }

        //Metoda do dodawania nowej kategorii
        public void AddCategory()
        {
            Console.Clear();

            Console.WriteLine("Dodawanie nowej kategorii:");

            //Pobranie nazwy kategorii
            Console.Write("Podaj nazwę kategorii: ");
            string categoryName = Console.ReadLine();

            //Pobranie kolejnego ID kategorii
            int nextCategoryId = new GetOrCreate().GetNextCategoryId();
            string newCategory = $"{nextCategoryId},{categoryName}";

            //Dodanie nowej kategorii do pliku
            File.AppendAllLines(categoriesFilePath, new[] { newCategory });

            Console.Clear();
            Console.WriteLine("Kategoria została dodana pomyślnie.");
            // Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }

        //Metoda do usuwania kategorii
        public void RemoveCategory()
        {
            Console.Clear();

            Console.WriteLine("Usuwanie kategorii:");

            //Pobranie ID kategorii do usunięcia
            Console.Write("Podaj ID kategorii do usunięcia: ");
            if (int.TryParse(Console.ReadLine(), out int categoryId))
            {
                //Odczytanie wszystkich kategorii z pliku
                string[] allCategories = File.ReadAllLines(categoriesFilePath);

                if (allCategories.Any(c => c.Split(',')[0] == categoryId.ToString()))
                {
                    //Usunięcie kategorii z bazy
                    string[] updatedCategories = allCategories.Where(c => c.Split(',')[0] != categoryId.ToString()).ToArray();
                    File.WriteAllLines(categoriesFilePath, updatedCategories);
                    Console.Clear();
                    Console.WriteLine("Kategoria została usunięta pomyślnie.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Kategoria o podanym ID nie istnieje.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nieprawidłowy format ID kategorii.");
            }

            //Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }

        //Metoda do edycji kategorii
        public void EditCategory()
        {
            Console.Clear();

            Console.WriteLine("Edycja kategorii:");

            //Pobranie ID kategorii do edycji
            Console.Write("Podaj ID kategorii do edycji: ");
            if (int.TryParse(Console.ReadLine(), out int categoryId))
            {
                //Odczytanie wszystkich kategorii z pliku
                string[] allCategories = File.ReadAllLines(categoriesFilePath);

                //Sprawdzenie, czy kategoria istnieje
                string category = allCategories.FirstOrDefault(c => c.Split(',')[0] == categoryId.ToString());
                if (category != null)
                {
                    //Pobranie nowej nazwy kategorii
                    Console.Write("Podaj nową nazwę kategorii: ");
                    string newCategoryName = Console.ReadLine();

                    //Zaktualizowanie nazwy kategorii
                    string[] updatedCategories = allCategories.Select(c =>
                    {
                        if (c.Split(',')[0] == categoryId.ToString())
                        {
                            return $"{categoryId},{newCategoryName}";
                        }
                        return c;
                    }).ToArray();

                    //Zapisanie zmienionej listy kategorii do pliku
                    File.WriteAllLines(categoriesFilePath, updatedCategories);

                    Console.Clear();
                    Console.WriteLine("Kategoria została zaktualizowana pomyślnie.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Kategoria o podanym ID nie istnieje.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nieprawidłowy format ID kategorii.");
            }
            // Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }

        //Metoda do dodawania nowego producenta
        public void AddManufacturer()
        {
            Console.Clear();

            Console.WriteLine("Dodawanie nowego producenta:");

            //Pobranie nazwy producenta
            Console.Write("Podaj nazwę producenta: ");
            string manufacturerName = Console.ReadLine();

            //Pobranie kolejnego ID producenta
            int nextManufacturerId = new GetOrCreate().GetNextManufacturerId();
            string newManufacturer = $"{nextManufacturerId},{manufacturerName}";

            //Dodanie nowego producenta do pliku
            File.AppendAllLines(manufacturersFilePath, new[] { newManufacturer });

            Console.Clear();
            Console.WriteLine("Producent został dodany pomyślnie.");
            //Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }

        //Metoda do usuwania producenta
        public void RemoveManufacturer()
        {
            Console.Clear();

            Console.WriteLine("Usuwanie producenta:");

            //Pobranie ID producenta do usunięcia
            Console.Write("Podaj ID producenta do usunięcia: ");
            if (int.TryParse(Console.ReadLine(), out int manufacturerId))
            {
                //Odczytanie wszystkich producentów z pliku
                string[] allManufacturers = File.ReadAllLines(manufacturersFilePath);

                if (allManufacturers.Any(m => m.Split(',')[0] == manufacturerId.ToString()))
                {
                    //Usunięcie producenta z bazy
                    string[] updatedManufacturers = allManufacturers.Where(m => m.Split(',')[0] != manufacturerId.ToString()).ToArray();
                    File.WriteAllLines(manufacturersFilePath, updatedManufacturers);
                    Console.Clear();
                    Console.WriteLine("Producent został usunięty pomyślnie.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Producent o podanym ID nie istnieje.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nieprawidłowy format ID producenta.");
            }
            // Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }

        public void EditManufacturer()
        {
            Console.Clear();

            Console.WriteLine("Edycja producenta:");

            //Pobranie ID producenta do edycji
            Console.Write("Podaj ID producenta do edycji: ");
            if (int.TryParse(Console.ReadLine(), out int manufacturerId))
            {
                //Odczytanie wszystkich producentów z pliku
                string[] allManufacturers = File.ReadAllLines(manufacturersFilePath);

                //Sprawdzenie, czy producent istnieje
                string manufacturer = allManufacturers.FirstOrDefault(m => m.Split(',')[0] == manufacturerId.ToString());
                if (manufacturer != null)
                {
                    //Pobranie nowej nazwy producenta
                    Console.Write("Podaj nową nazwę producenta: ");
                    string newManufacturerName = Console.ReadLine();

                    //Zaktualizowanie nazwy producenta
                    string[] updatedManufacturers = allManufacturers.Select(m =>
                    {
                        if (m.Split(',')[0] == manufacturerId.ToString())
                        {
                            return $"{manufacturerId},{newManufacturerName}";
                        }
                        return m;
                    }).ToArray();

                    //Zapisanie zmienionej tablicy do pliku
                    File.WriteAllLines(manufacturersFilePath, updatedManufacturers);

                    Console.Clear();
                    Console.WriteLine("Producent został zaktualizowany pomyślnie.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Producent o podanym ID nie istnieje.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nieprawidłowy format ID producenta.");
            }
            // Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }

        //Metoda do edycji danych użytkownika
        public void EditUser()
        {
            Console.Clear();
            Console.WriteLine("Edycja danych użytkownika:");

            //Pobranie nazwy użytkownika do edycji
            Console.Write("Podaj nazwę użytkownika do edycji: ");
            string username = Console.ReadLine();

            //Odczytanie wszystkich użytkowników z pliku
            string[] allUsers = File.ReadAllLines(usersFilePath);

            //Sprawdzenie, czy użytkownik istnieje
            string user = allUsers.FirstOrDefault(u => u.Split(',')[1] == username);
            if (user != null)
            {
                //Pobranie nowej roli użytkownika
                Console.Write("Nowa rola użytkownika (Admin/Klient): ");
                string newRole = Console.ReadLine();

                //Znalezienie indeksu użytkownika w tablicy
                int index = Array.FindIndex(allUsers, u => u.Split(',')[1] == username);

                //Zaktualizowanie danych użytkownika
                allUsers[index] = $"{user.Split(',')[0]},{username},{user.Split(',')[2]},{user.Split(',')[3]},{newRole}";

                //Zapisanie zmienionej tablicy do pliku
                File.WriteAllLines(usersFilePath, allUsers);

                Console.WriteLine("Dane użytkownika zostały zaktualizowane pomyślnie.");
            }
            else
            {
                Console.WriteLine("Użytkownik o podanej nazwie nie istnieje.");
            }
            Console.Clear();
            // Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }

        public void RemoveUser()
        {
            Console.Clear();

            Console.WriteLine("Usuwanie klienta:");

            //Pobranie nazwy użytkownika do usunięcia
            Console.Write("Podaj nazwę użytkownika do usunięcia: ");
            string username = Console.ReadLine();

            //Odczytanie wszystkich użytkowników z pliku
            string[] allUsers = File.ReadAllLines(usersFilePath);

            //Sprawdzenie, czy użytkownik istnieje
            string user = allUsers.FirstOrDefault(u => u.Split(',')[1] == username);
            if (user != null)
            {
                //Usunięcie użytkownika z bazy
                string[] updatedUsers = allUsers.Where(u => u.Split(',')[1] != username).ToArray();
                File.WriteAllLines(usersFilePath, updatedUsers);

                Console.Clear();
                Console.WriteLine("Użytkownik został usunięty pomyślnie.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Użytkownik o podanej nazwie nie istnieje.");
            }
            Console.Clear();
            // Powrót do głównego menu administratora
            new LoginResult().AdminOptions();
        }
    }
}
