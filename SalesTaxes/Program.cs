using SalesTaxes.Models;
using SalesTaxes.Services;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SalesTaxes
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            var basket = GetUserInput();
            var receipt = ProcessBasket(basket);
            PrintReceipt(receipt);
        }

        static List<Item> GetUserInput()
        {
            List<Item> items = new List<Item>();
            Console.WriteLine("Enter item details (type 'done' to finish):");

            while (true)
            {
                Console.WriteLine("Enter item name (or 'done' to finish):");
                string? inputName = Console.ReadLine();
                if (string.IsNullOrEmpty(inputName))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                    continue;
                }
                if (inputName.ToLower() == "done")
                {
                    break;
                }

                Console.WriteLine("Enter shelf price:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
                {
                    Console.WriteLine("Invalid price. Please enter a positive decimal number.");
                    continue;
                }

                Console.WriteLine("Is the item imported? (yes/no):");
                string? inputImported = Console.ReadLine();
                bool isImported = inputImported != null && inputImported.ToLower() == "yes";

                Console.WriteLine("Is the item exempt from basic sales tax? (yes/no):");
                string? inputExempt = Console.ReadLine();
                bool isExempt = inputExempt != null && inputExempt.ToLower() == "yes";

                items.Add(new Item
                {
                    Name = inputName,
                    ShelfPrice = price,
                    IsImported = isImported,
                    IsExempt = isExempt
                });
            }

            return items;
        }

        static Receipt ProcessBasket(List<Item> items)
        {
            var receipt = new Receipt();
            foreach (var item in items)
            {
                decimal tax = TaxCalculator.CalculateTax(item);
                item.ShelfPrice += tax; 
                receipt.Items.Add(item);
                receipt.TotaltSalesTax += tax;
                receipt.Totalt += item.ShelfPrice;
            }

            return receipt;
        }

        static void PrintReceipt(Receipt receipt)
        {
            foreach (var item in receipt.Items)
            {
                Console.WriteLine($"{(item.IsImported ? "Imported " : "")}{item.Name}: {item.ShelfPrice:F2}");
            }
            Console.WriteLine($"Sales Taxes: {receipt.TotaltSalesTax:F2}");
            Console.WriteLine($"Total: {receipt.Totalt:F2}");
        }
    }
}
