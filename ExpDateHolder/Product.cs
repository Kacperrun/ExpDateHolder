using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpDateHolder
{
    class Product
    {
        public List<DateTime> NearestExpirationDate { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }
        public string Ean { get; private set; }
        public bool CountMoreThan0 { get; private set; }

        public Product(string ean, string name, string location, DateTime date)
        {
            Ean = ean;
            Name = name;
            Location = location;
            NearestExpirationDate = new List<DateTime>();
            NearestExpirationDate.Add(date);
            MakeCountMoreThan0();
        }

        public void MakeCount0()
        {
            CountMoreThan0 = false;
        }

        public void MakeCountMoreThan0()
        {
            CountMoreThan0 = true;
        }

        public override string ToString()
        {
            return Name + " | " + Ean + " | " + Location + " | "  + NearestExpirationDate.ElementAt(NearestExpirationDate.Count - 1).Date.ToString();
        }

        /// <summary>
        /// Sprawdza, czy dany produkt spełnia warunki do przeceny
        /// </summary>
        /// <param name="days">Ilość dni, za ile kończy się termin przydatności produktów, które chcemy przecenić</param>
        /// <returns>Prawdę, jeśli data produktu jest mniejsza lub równa względem daty, do której produkt jest do przeceny</returns>
        public bool IsItToPromo(int days)
        {
            DateTime date = new DateTime();
            date = NearestExpirationDate.ElementAt(0);

            if (date <= (DateTime.Now.AddDays(days)))
                return true;
            return false;
        }

        /// <summary>
        /// Zmienia najbliższą datę przydatności do spożycia
        /// </summary>
        public void ChangeNearestExpirationDate()
        {
            Console.Clear();

            Console.WriteLine("**************************************************");
            Console.WriteLine("Produkt: " + Name);
            Console.WriteLine("Obecna data: " + NearestExpirationDate.Last().Date.Date);
            Console.WriteLine("Nowa data");
            Security.ParseDate(out int day, out int month, out int year);
            NearestExpirationDate.Add(new DateTime(year, month, day));
            NearestExpirationDate.RemoveAt(0);
           
        }

    }
}
