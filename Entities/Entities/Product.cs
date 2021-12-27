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
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ShortDescription { get; set; }
        public string Specifications { get; set; }
        public int Quantity { get; set; }
        public int ProductUnitTypeId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ProductUnitType ProductUnitType { get; set; }
        public virtual ICollection<InventoryLog> InventoryLogs { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}
