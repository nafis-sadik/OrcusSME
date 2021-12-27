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
            try
            {
                _productUnitTypeRepo.Add(new ProductUnitType
                {
                    UnitTypeIds = _productUnitTypeRepo.GetMaxPK("UnitTypeIds") + 1,
                    UnitTypeNames = productUnitType.UnitTypeName,
                    Status = CommonConstants.StatusTypes.Active
                });
                return true;
            }
            catch (Exception ex)
            {
                _productUnitTypeRepo.Rollback();

                int pk;
                if (_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                if (ex.InnerException != null)
                    _crashLogRepo.Add(new Crashlog
                    {
                        CrashLogId = pk,
                        ClassName = "ProductService",
                        MethodName = "AddProductUnitTypes",
                        ErrorMessage = ex.Message,
                        ErrorInner =
                            (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException
                                ? ex.InnerException.Message
                                : ex.Message),
                        Data = productUnitType.UserId,
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
                Product productData;
                int count;
                if (product.ProductId != 0)
                {
                    productData = _productRepo.Get(product.ProductId);
                    productData.ProductName = product.ProductName;
                    productData.CategoryId = product.CategoryId;
                    productData.Description = product.ProductDescription;
                    productData.ProductUnitTypeId = product.UnitType;
                    productData.Quantity += product.Quantity;
                    productData.Price = product.SellingPrice;
                    _productRepo.Update(productData);
                }
                else
                {
                    count = _productRepo.AsQueryable().Count();
                    if (count <= 0)
                        pk = 1;
                    else
                        pk = count + 1;
                    productData = new Product();
                    productData.ProductId = pk;
                    productData.ProductName = product.ProductName;
                    productData.CategoryId = product.CategoryId;
                    productData.Description = product.ProductDescription;
                    productData.ShortDescription = product.ShortDescription;
                    productData.Specifications = product.ProductSpecs;
                    productData.Price = product.SellingPrice;
                    productData.ProductUnitTypeId = product.UnitType;
                    productData.Quantity = product.Quantity;
                    _productRepo.Add(productData);
                }

                count = _inventoryLogRepo.AsQueryable().Count();
                if (count <= 0)
                    pk = 1;
                else
                    pk = count + 1;

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

                if (_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                if (ex.InnerException != null)
                    _crashLogRepo.Add(new Crashlog
                    {
                        CrashLogId = pk,
                        ClassName = "ProductService",
                        MethodName = "PurchaseProduct",
                        ErrorMessage = ex.Message,
                        ErrorInner =
                            (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException
                                ? ex.InnerException.Message
                                : ex.Message),
                        Data = JsonSerializer.Serialize(product),
                        TimeStamp = DateTime.Now
                    });
                return false;
            }
        }

        public bool SellProduct(ProductModel product)
        {
            int pk;
            try{
                Product productData = _productRepo.Get(product.ProductId);
                productData.Quantity -= product.Quantity;
                _productRepo.Update(productData);
                

                int count = _inventoryLogRepo.AsQueryable().Count();
                if (count <= 0)
                    pk = 1;
                else
                    pk = count + 1;

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

                if (_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                if (ex.InnerException != null)
                    _crashLogRepo.Add(new Crashlog
                    {
                        CrashLogId = pk,
                        ClassName = "ProductService",
                        MethodName = "SellProduct",
                        ErrorMessage = ex.Message,
                        ErrorInner =
                            (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException
                                ? ex.InnerException.Message
                                : ex.Message),
                        Data = JsonSerializer.Serialize(product),
                        TimeStamp = DateTime.Now
                    });
                return false;
            }
        }
    }
}
