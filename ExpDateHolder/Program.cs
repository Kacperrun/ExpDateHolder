using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;

namespace ExpDateHolder
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = Service.PrepareList();
            products.ElementAt(products.Count - 1).MakeCount0();
            char choice;

            do
            {
                Console.Clear();
                Service.ShowMenu();
                choice = Convert.ToChar(Console.ReadLine());


                switch (choice)
                {
                    case '1': Service.AddNewProduct(ref products); break;
                    case '2': Service.DeleteProduct(ref products); break;
                    case '3': Service.ShowProductsToPromo(products); break;
                    case '4': Service.ShowProductWithMaxDate(products); break;
                    case '5': Service.ChangeDates(ref products); break;
                    case '6': Service.ChangeDateOfProduct(ref products); break;
                    case '7': Service.AddDatesOf0CountProducts(ref products); break;
                    case '8': Service.Show0CountProducts(products); break;
                    case '9': Service.MakeCount0(ref products); break;
                    case '0': Environment.Exit(0); break;
                    default: Console.WriteLine(""); break;
                }

            } while (choice != '0');


        }
    }
}
