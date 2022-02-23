namespace DataLayer.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int OutletId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string ProductDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ProductSpecs { get; set; }
        public string OutletName { get; set; }
        public int UnitTypeId { get; set; }
        public int UnitType { get; set; }
        public int Quantity { get; set; }
        public int PurchasingPrice { get; set; }
        public int RetailPrice { get; set; }
        public float DueAmount { get; set; }
        public string UserId { get; set; }
        public int [] ProductImageIds { get; set; }
    }
}
