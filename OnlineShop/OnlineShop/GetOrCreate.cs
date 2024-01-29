using System;

namespace OnlineShop
{
    internal class GetOrCreate
    {
        //Ścieżki do plików CSV przechowujących dane
        private const string productsFilePath = "Data/Products.csv";
        private const string categoriesFilePath = "Data/Categories.csv";
        private const string manufacturersFilePath = "Data/Manufacturers.csv";
        private const string usersFilePath = "Data/Users.csv";

        //Metoda do pobierania istniejącego identyfikatora producenta lub tworzenia nowego
        public int GetOrCreateManufacturer(string manufacturerName)
        {
            string[] allManufacturers = File.ReadAllLines(manufacturersFilePath);
            string manufacturer = allManufacturers.FirstOrDefault(m => m.Split(',')[1].Equals(manufacturerName, StringComparison.OrdinalIgnoreCase));

            if (manufacturer != null)
            {
                return int.Parse(manufacturer.Split(',')[0]);
            }

            int nextManufacturerId = GetNextManufacturerId();
            string newManufacturer = $"{nextManufacturerId},{manufacturerName}";
            File.AppendAllLines(manufacturersFilePath, new[] { newManufacturer });

            return nextManufacturerId;
        }

        //Metoda do pobierania istniejącego identyfikatora kategorii lub tworzenia nowego
        public int GetOrCreateCategory(string categoryName)
        {
            string[] allCategories = File.ReadAllLines(categoriesFilePath);
            string category = allCategories.FirstOrDefault(c => c.Split(',')[1].Equals(categoryName, StringComparison.OrdinalIgnoreCase));

            if (category != null)
            {
                return int.Parse(category.Split(',')[0]);
            }

            int nextCategoryId = GetNextCategoryId();
            string newCategory = $"{nextCategoryId},{categoryName}";
            File.AppendAllLines(categoriesFilePath, new[] { newCategory });

            return nextCategoryId;
        }

        //Metoda do pobierania kolejnego dostępnego identyfikatora produktu
        public int GetNextProductId()
        {
            string[] allProducts = File.ReadAllLines(productsFilePath);
            if (allProducts.Length == 0)
            {
                return 1;
            }

            int lastProductId = int.Parse(allProducts.Last().Split(',')[0]);
            return lastProductId + 1;
        }

        //Metoda do pobierania kolejnego dostępnego identyfikatora kategorii
        public int GetNextCategoryId()
        {
            string[] allCategories = File.ReadAllLines(categoriesFilePath);
            if (allCategories.Length == 0)
            {
                return 1;
            }

            int lastCategoryId = int.Parse(allCategories.Last().Split(',')[0]);
            return lastCategoryId + 1;
        }

        //Metoda do pobierania kolejnego dostępnego identyfikatora producenta
        public int GetNextManufacturerId()
        {
            string[] allManufacturers = File.ReadAllLines(manufacturersFilePath);
            if (allManufacturers.Length == 0)
            {
                return 1;
            }

            int lastManufacturerId = int.Parse(allManufacturers.Last().Split(',')[0]);
            return lastManufacturerId + 1;
        }

        //Metoda statyczna do pobierania kolejnego dostępnego identyfikatora użytkownika
        public static int GetNextUserID()
        {
            string[] allUsers = File.ReadAllLines(usersFilePath);
            if (allUsers.Length == 0)
            {
                return 1;
            }

            int lastUserID = int.Parse(allUsers.Last().Split(',')[0]);
            return lastUserID + 1;
        }
    }
}
