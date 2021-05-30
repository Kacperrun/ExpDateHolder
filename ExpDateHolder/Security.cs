using System;

namespace ExpDateHolder
{
    static class Security
    {
        static public int ParseInt()
        {
            bool isItInt = int.TryParse(Console.ReadLine(), out int num);

            while (!isItInt)
            {
                Console.Write("Nie podano liczby. Spróbuj ponownie: ");
                isItInt = int.TryParse(Console.ReadLine(), out num);
            }

            return num;
        }

        static public void ParseDate(out int day, out int month, out int year)
        {

            bool success;
            do
            {
                Console.Write("Dzień: ");
                day = ParseInt();
                Console.Write("Miesiąc: ");
                month = ParseInt();
                Console.Write("Rok: ");
                year = ParseInt();


                bool monthWith31days;

                switch (month)
                {
                    case 1: monthWith31days = true; break;
                    case 3: monthWith31days = true; break;
                    case 5: monthWith31days = true; break;
                    case 7: monthWith31days = true; break;
                    case 8: monthWith31days = true; break;
                    case 10: monthWith31days = true; break;
                    case 12: monthWith31days = true; break;
                    default: monthWith31days = false; break;
                }

                if (monthWith31days)
                {
                    if (day >= 1 && day <= 31)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                        Console.WriteLine("Wprowadź poprawny dzień");
                        continue;
                    }
                }
                else
                {
                    if (day >= 1 && day <= 30)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                        Console.WriteLine("Wprowadź poprawny dzień");
                        continue;
                    }
                }

                if (month == 2)
                {
                    if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
                    {
                        if (day >= 1 && day <= 29)
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                            Console.WriteLine("Wprowadź poprawny dzień");
                            continue;
                        }
                    }
                    else
                    {
                        if (day >= 1 && day <= 28)
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                            Console.WriteLine("Wprowadź poprawny dzień");
                            continue;
                        }
                    }

                }

                if (month >= 1 && month <= 12)
                {
                    success = true;
                }
                else
                {
                    success = false;
                    Console.WriteLine("Wprowadź poprawny miesiąc");
                    continue;
                }

            } while (!success);
        }
    }
}

