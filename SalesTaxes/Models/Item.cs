namespace SalesTaxes.Models
{
    //Represents an Item being Purchased 
    public class Item
    {
        public string Name { get; set; }
        public decimal ShelfPrice { get; set; }
        public bool IsImported { get; set; }
        public bool IsExempt { get; set; }

    }

}
