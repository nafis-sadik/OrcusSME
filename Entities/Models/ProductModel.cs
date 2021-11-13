namespace DataLayer.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int OutletId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int PurchasingPrice { get; set; }
        public int ProductDescription { get; set; }
        public string OutletName { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
        public int Quantity { get; set; }
        public int PurchasePrice { get; set; }
        public int SellingPrice { get; set; }
    }
}
