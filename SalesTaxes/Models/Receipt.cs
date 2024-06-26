using System.Collections.Generic;
namespace SalesTaxes.Models
{

    // Represents a receipt that contains multiple  items and totals 
    public class Receipt
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public decimal TotaltSalesTax { get; set; }
        public decimal Totalt { get; set; }
    }

}