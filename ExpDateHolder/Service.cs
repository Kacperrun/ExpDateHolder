using System;
using System.Collections.Generic;

namespace ExpDateHolder
{
    static class Service
    {
        static public List<Product> PrepareList()
        {
            List<Product> products = new List<Product>
            {
                new Product("2000000006666", "Jabłka torba 2,5 kg", "Warzywa 7/3/1", new DateTime(2020, 12, 2)),
                new Product("53510", "Chleb Oliwski Krojony Kropidłowski 500 g", "Pieczywo 3/1/7", new DateTime(2020, 11, 22)),
                new Product("2908743569871", "Masło Extra Polskie Mlekovita 200 g", "Nabiał 1/3/1", new DateTime(2020, 12, 1)),
                new Product("12615", "Bułka kajzerka Bochen", "Pieczywo 2/3/2", new DateTime(2020, 11, 25))
            };
            return products;
        }

        static public void ShowProductsWithDate(List<Product> products, int days)
        {
            Console.Clear();
            foreach (var item in products)
            {
                if (item.IsItToPromo(days) && item.CountMoreThan0)
                {
                    Console.WriteLine(item.ToString());
                }
            }

            Console.ReadKey();
        }
        static public void ShowProductsToPromo(List<Product> products)
        {
            Console.Clear();
            Console.Write("Z jakim wyprzedzeniem w dniach chcesz znaleźć produkty: ");
            
            int.TryParse(Console.ReadLine(), out int days);

            Service.ShowProductsWithDate(products, days);
        }

        static public void DeleteProduct(ref List<Product> products)
        {
            Console.Clear();
            Console.Write("Podaj kod EAN produktu do usunięcia: ");
            string searchedEan = Console.ReadLine();

            bool success = false;
            int i = 0;

            foreach (var item in products)
            { 
                if(String.Equals(searchedEan, item.Ean))
                {
                    success = true;
                    break;
                }

                i++;
            }

            if(success)
            {
                products.RemoveAt(i);
            }
            else
            {
                Console.WriteLine("Nie znaleziono produktu o podanym kodzie");
            }

        }

        /// <summary>
        /// Wyświetla wszystkie produkty z maksymalną datą przydatności do spożycia określoną przez użytkownika
        /// </summary>
        /// <param name="products">Lista wszystkich produktów</param>
        static public void ShowProductWithMaxDate(List<Product> products)
        {
            Console.Clear();
            Console.WriteLine("Podaj datę, do której chcesz wyświetlić produkty");
            Security.ParseDate(out int day, out int month, out int year);
            DateTime date = new DateTime(year, month, day);
            Service.ShowProductsWithDate(products, Convert.ToInt32((date - DateTime.Now).Days));
        }

        static public void Show0CountProducts(List<Product> products)
        {
            Console.Clear();
            int count = 0;
            
            foreach (var item in products)
            {
                if(!item.CountMoreThan0)
                {
                    count++;
                    Console.WriteLine(item.ToString());
                }
            }

            if(count == 0)
            {
                Console.WriteLine("Nie znaleziono produktów z zerowym stanem magazynowym");
            }

            Console.ReadKey();

        }
        static public void ShowMenu()
        {
            Console.Clear();

            Console.WriteLine("*** MENU GŁÓWNE ***");
            Console.WriteLine("1. Dodaj nowy produkt");
            Console.WriteLine("2. Usuń produkt");
            Console.WriteLine("3. Wyświetl produkty do przeceny");
            Console.WriteLine("4. Wyświetl produkty z określoną max datą");
            Console.WriteLine("5. Zmień najbliższe daty przydatności produtów przecenionych");
            Console.WriteLine("6. Zmień najbliższą datę przydatności konkretnego produktu");
            Console.WriteLine("7. Dodaj daty przydatności produktów stanu zerowego");
            Console.WriteLine("8. Wyświetl produty stanu zerowego");
            Console.WriteLine("9. Wyzeruj stan produktu");
            Console.WriteLine("0. Wyjście");
        }

        static public void MakeCount0(ref List<Product> products)
        {
            Console.Clear();
            Console.Write("Podaj kod EAN produktu do wyzerowania stanu magazynowego: ");
            string ean = Console.ReadLine();
            bool success = false;

            foreach (var item in products)
            {
                if(String.Equals(ean, item.Ean))
                {
                    item.MakeCount0();
                    success = true;
                    Console.WriteLine("Pomyślnie wyzerowano stan magazynowy produktu\n" + item.ToString());
                }

            }

            if(!success)
            {
                Console.WriteLine("Nie znaleziono produktu o podanym kodzie EAN");
            }

            Console.ReadKey();

        }

        static public void AddDatesOf0CountProducts(ref List<Product> products)
        {
            Console.Clear();

            foreach (var item in products)
            {
                if(!item.CountMoreThan0)
                {
                    Console.WriteLine(item.ToString());
                    Console.Write("Czy chcesz dodać datę (+/-) ");
                    char choice = Convert.ToChar(Console.Read());
                    if(choice == '+')
                    {
                        item.ChangeNearestExpirationDate();
                        item.MakeCountMoreThan0();
                    }
                }
            }

        }

        static public void ChangeDateOfProduct(ref List<Product> products)
        {
            Console.Clear();
            Console.WriteLine("Zmień najbliższą datę przydatności produktu");
            Console.Write("Podaj kod EAN produktu: ");
            string ean = Console.ReadLine();

            bool found = false;
            foreach (var item in products)
            {
                if(String.Equals(ean, item.Ean))
                {
                    item.ChangeNearestExpirationDate();
                    found = true;
                    break;
                }
            }

            if(!found)
            {
                
                Console.WriteLine("Nie znaleziono takiego produktu");
                Console.ReadKey();
            }

        }
        static public void ChangeDates(ref List<Product> products)
        {
            int changes = 0;
            Console.Clear();
            Console.WriteLine("Podaj max datę produktów przecenionych");
            Security.ParseDate(out int day, out int month, out int year);
         
            foreach (var item in products)
            {
                if (item.IsItToPromo(Convert.ToInt32((new DateTime(year, month, day) - DateTime.Now).Days)))
                {
                    changes++;
                    item.ChangeNearestExpirationDate();
                }
            }
            Console.Clear();
            Console.WriteLine("Pomyślnie zmieniono wszystkie daty (" + changes.ToString() + ")" );
            Console.ReadKey();


        }


        /// <summary>
        /// Dodawanie nowych produktów do bazy danych. W przypadku nowych produktów w sklepie, używamy właśnie tej metody do dopisania produuktu do listy.
        /// </summary>
        /// <param name="products">Nasza istniejąca już baza produktów</param>
        static public void AddNewProduct(ref List<Product> products)
        {

            string name;
            string ean;
            string location;
            int day;
            int month;
            int year;

            Console.Clear();
            bool success = true;
            
            do
            {
                Console.WriteLine("****************************");
                Console.WriteLine("Wprowadzanie nowego produktu");
                Console.Write("Nazwa: ");
                name = Console.ReadLine();
                Console.Write("Kod EAN: ");
                ean = Console.ReadLine();
                Console.Write("Lokalizacja: ");
                location = Console.ReadLine();
                Console.WriteLine("Data przydatności");
                Security.ParseDate(out day, out month, out year);

                foreach (var item in products)
                {
                    if(String.Equals(item.Ean, ean))
                    {
                        success = false;
                        break;
                    }
                }

                if(!success)
                {
                    Console.WriteLine("\aBłąd. W bazie istnieje już produkt o podanym kodzie EAN");
                    Console.ReadKey();
                }

                Console.Clear();


            } while (!success);

            products.Add(new Product(ean, name, location, new DateTime(year, month, day)));
        }
    }
}
