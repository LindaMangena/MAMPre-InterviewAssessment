using SalesTaxes.Models;
using System;

namespace SalesTaxes.Services
{
    public class TaxCalculator
    {
        public static decimal CalculateTax(Item item)
        {
            decimal basicTax = 0m;
            if (!item.IsExempt)
            {
                basicTax = RoundUp(item.ShelfPrice * 0.10m);
            }

            decimal importDuty = 0m;
            if (item.IsImported)
            {
                importDuty = RoundUp(item.ShelfPrice * 0.05m);
            }

            return basicTax + importDuty;
        }

        private static decimal RoundUp(decimal value)
        {
            return Math.Ceiling(value * 20) / 20; 
        }
    }
}
