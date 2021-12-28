using DataLayer;
using DataLayer.Entities;
using DataLayer.Models;
using DataLayer.MSSQL;
using DataLayer.MySql;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Orcus.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Orcus.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductUnitTypeRepo _productUnitTypeRepo;
        private readonly ICrashLogRepo _crashLogRepo;
        private readonly IInventoryLogRepo _inventoryLogRepo;
        private readonly IProductRepo _productRepo;

        public ProductService()
        {
            OrcusSMEContext context = new OrcusSMEContext(new DbContextOptions<OrcusSMEContext>());
            _productUnitTypeRepo = new ProductUnitTypeRepo(context);
            _crashLogRepo = new CrashLogRepo(context);
            _inventoryLogRepo = new InventoryLogRepo(context);
            _productRepo = new ProductRepo(context);
        }

        public IEnumerable<ProductUnitTypeModel> GetProductUnitTypes()
        {
            return _productUnitTypeRepo.AsQueryable().Where(x => x.Status == CommonConstants.StatusTypes.Active).Select(x =>
                new ProductUnitTypeModel {
                    UnitTypeId = x.UnitTypeIds,
                    UnitTypeName = x.UnitTypeNames
                });
        }

        public bool AddProductUnitTypes(ProductUnitTypeModel productUnitType)
        {
            int pk;
            try
            {
                if (!_productUnitTypeRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _productUnitTypeRepo.GetMaxPK("UnitTypeIds");

                _productUnitTypeRepo.Add(new ProductUnitType
                {
                    UnitTypeIds = pk + 1,
                    UnitTypeNames = productUnitType.UnitTypeName,
                    Status = CommonConstants.StatusTypes.Active
                });

                return true;
            }
            catch (Exception ex)
            {
                _productUnitTypeRepo.Rollback();

                if (!_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.GetMaxPK("CrashLogId") + 1;

                string msg = (string.IsNullOrEmpty(ex.Message) || ex.Message.ToLower().Contains(CommonConstants.MsgInInnerException.ToLower()))
                            ? ex.InnerException.Message
                            : ex.Message;
                _crashLogRepo.Add(new Crashlog
                {
                    CrashLogId = pk,
                    ClassName = "ProductService",
                    MethodName = "AddProductUnitTypes",
                    ErrorMessage = ex.Message,
                    ErrorInner = msg,
                    Data = JsonSerializer.Serialize(productUnitType),
                    TimeStamp = DateTime.Now
                });

                return false;
            }
        }

        public bool PurchaseProduct(ProductModel product)
        {
            int pk;
            try
            {
                Product productData = new Product();
                if (product.ProductId != 0)
                    productData = _productRepo.Get(product.ProductId);
                else
                {
                    if (!_productRepo.AsQueryable().Any())
                        productData.ProductId = 1;
                    else
                        productData.ProductId = _productRepo.AsQueryable().Count() + 1;
                }
                productData.ProductName = product.ProductName;
                productData.CategoryId = product.CategoryId;
                productData.Description = product.ProductDescription;
                productData.ProductUnitTypeId = product.UnitType;
                productData.Price = product.SellingPrice;
                productData.Quantity = product.Quantity;
                productData.ShortDescription = product.ShortDescription;
                productData.Specifications = product.ProductSpecs;

                if (product.ProductId != 0)
                    _productRepo.Update(productData);
                else
                    _productRepo.Add(productData);

                if (!_inventoryLogRepo.AsQueryable().Any())
                    pk = 1;
                else
                    pk = _inventoryLogRepo.AsQueryable().Count() + 1;

                _inventoryLogRepo.Add(new InventoryLog
                {
                    InventoryLogId = pk,
                    ActivityDate = DateTime.Now,
                    InventoryUpdateType = CommonConstants.ActivityTypes.Purchase,
                    Price = product.PurchasingPrice,
                    ProductId = productData.ProductId,
                    Quantity = productData.Quantity,
                });

                return true;
            }
            catch (Exception ex)
            {
                _productUnitTypeRepo.Rollback();

                if (!_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.GetMaxPK("CrashLogId") + 1;

                string msg = (string.IsNullOrEmpty(ex.Message) || ex.Message.ToLower().Contains(CommonConstants.MsgInInnerException.ToLower()))
                            ? ex.InnerException.Message
                            : ex.Message;
                _crashLogRepo.Add(new Crashlog
                {
                    CrashLogId = pk,
                    ClassName = "ProductService",
                    MethodName = "PurchaseProduct",
                    ErrorMessage = ex.Message,
                    ErrorInner = msg,
                    Data = JsonSerializer.Serialize(product),
                    TimeStamp = DateTime.Now
                });

                return false;
            }
        }

        public bool? SellProduct(ProductModel product)
        {
            int pk;
            try{
                Product productData = _productRepo.Get(product.ProductId);
                if (productData == null) return null;
                productData.Quantity -= product.Quantity;
                _productRepo.Update(productData);
                

                if (!_inventoryLogRepo.AsQueryable().Any())
                    pk = 1;
                else
                    pk = _inventoryLogRepo.AsQueryable().Count() + 1;

                _inventoryLogRepo.Add(new InventoryLog
                {
                    InventoryLogId = pk,
                    ActivityDate = DateTime.Now,
                    InventoryUpdateType = CommonConstants.ActivityTypes.Sell,
                    Price = product.SellingPrice,
                    ProductId = productData.ProductId,
                    Quantity = productData.Quantity,
                });

                return true;
            }
            catch (Exception ex)
            {
                _productUnitTypeRepo.Rollback();

                if (!_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.GetMaxPK("CrashLogId") + 1;

                string msg = (string.IsNullOrEmpty(ex.Message) || ex.Message.ToLower().Contains(CommonConstants.MsgInInnerException.ToLower()))
                            ? ex.InnerException.Message
                            : ex.Message;
                _crashLogRepo.Add(new Crashlog
                {
                    CrashLogId = pk,
                    ClassName = "ProductService",
                    MethodName = "SellProduct",
                    ErrorMessage = ex.Message,
                    ErrorInner = msg,
                    Data = JsonSerializer.Serialize(product),
                    TimeStamp = DateTime.Now
                });

                return false;
            }
        }
    }
}
