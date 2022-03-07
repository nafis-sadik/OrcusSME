using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class Product
    {
        public Product()
        {
            InventoryLogs = new HashSet<InventoryLog>();
            ProductAttributes = new HashSet<ProductAttribute>();
            ProductPictures = new HashSet<ProductPicture>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public double RetailPrice { get; set; }
        public string ShortDescription { get; set; }
        public string Specifications { get; set; }
        public int Quantity { get; set; }
        public int UnitTypeId { get; set; }
        public string Status { get; set; }

        public virtual Category Category { get; set; }
        public virtual ProductUnitType UnitType { get; set; }
        public virtual ICollection<InventoryLog> InventoryLogs { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
    }
}
