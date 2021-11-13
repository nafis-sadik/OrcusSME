using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class PurchaseModel : BaseModel
    {
        public int ProductId { get; set; }
        public int OutletId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int ProductName { get; set; }
        public int UnitId { get; set; }
        public int Quantity { get; set; }
        public float PurchasingPrice { get; set; }
        public float RetailPrice { get; set; }
        public float DueAmount { get; set; }
        public string ProductDescription { get; set; }
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<ProductUnitType> ProductUnitTypes { get; set; }
    }
}
