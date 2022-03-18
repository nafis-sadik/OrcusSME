using DataLayer.Entities;
using DataLayer.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Orcus.Abstraction
{
    public interface IProductService
    {
        public IEnumerable<ProductUnitTypeModel> GetProductUnitTypes();
        public bool AddProductUnitTypes(ProductUnitTypeModel productUnitType);
        public int? SaveProduct(ProductModel product);
        public bool PurchaseProduct(int productId, int quantity, double purchasingPrice);
        public bool? SellProduct(ProductModel product);
        public IEnumerable<ProductModel> GetInventory(OutletModel outletModel);
        public bool? ArchiveProduct(string userId, int productId);
        public ProductModel GetProductById(int productId);
    }
}
