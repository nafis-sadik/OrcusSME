using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class ProductUnitType
    {
        public ProductUnitType()
        {
            Products = new HashSet<Product>();
        }

        public int UnitTypeIds { get; set; }
        public string UnitTypeNames { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
