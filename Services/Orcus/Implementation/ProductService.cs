using DataLayer;
using DataLayer.Entities;
using DataLayer.Models;
using DataLayer.MSSQL;
using DataLayer.MySql;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Implementation;
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
        private readonly IOutletManagerRepo _outletManagerRepo;

        public ProductService()
        {
            OrcusSMEContext context = new OrcusSMEContext(new DbContextOptions<OrcusSMEContext>());
            _productUnitTypeRepo = new ProductUnitTypeRepo(context);
            _crashLogRepo = new CrashLogRepo(context);
            _inventoryLogRepo = new InventoryLogRepo(context);
            _productRepo = new ProductRepo(context);
            _outletManagerRepo = new OutletManagerRepo(context);
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
                if (product.SubCategoryId != 0)
                    productData.CategoryId = product.SubCategoryId;
                else
                    productData.CategoryId = product.CategoryId;
                productData.Description = product.ProductDescription;
                productData.ProductUnitTypeId = product.UnitType;
                productData.Price = product.RetailPrice;
                productData.Quantity = product.Quantity;
                productData.ShortDescription = product.ShortDescription;
                productData.Specifications = product.ProductSpecs;
                productData.ProductUnitTypeId = product.UnitId;

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
                    Price = product.RetailPrice,
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

        public IEnumerable<ProductModel> GetInventory(ProductModel productModel)
        {
            int pk;
            IEnumerable<ProductModel> response = new List<ProductModel>();
            try
            {
                // Return all outlets when no outlet selected
                if (productModel.OutletId <= 0)
                    return null;
                else
                {
                    // Check if the person owns the outlet or not
                    Outlet outlet = _outletManagerRepo.Get(productModel.OutletId);
                    if (outlet.UserId != productModel.UserId)
                        return null;
                    // Get all products of the outlet
                    IQueryable<Product> products = _productRepo.AsQueryable().Where(x => x.Category.OutletId == productModel.OutletId);
                    List<ProductModel> productsList = new List<ProductModel>();
                    int productCounter, sellCounter;
                    // Convert Entities into Models
                    foreach (Product product in products)
                    {
                        productCounter = 0;
                        sellCounter = 0;
                        // Calculate inventory size from inventory log
                        var records = _inventoryLogRepo.AsQueryable().Where(x => x.ProductId == product.ProductId).Select(x => new { x.InventoryUpdateType, x.Quantity }).ToList();
                        foreach (var data in records)
                        {
                            if (data.InventoryUpdateType == CommonConstants.ActivityTypes.Purchase)
                                productCounter += data.Quantity;
                            else
                            {
                                productCounter -= data.Quantity;
                                sellCounter++;
                            }
                        }
                        productsList.Add(new ProductModel
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            Quantity = productCounter,
                            PurchasingPrice = 0,
                            RetailPrice = 0,
                            OutletName = outlet.OutletName
                        });
                    }
                }
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
                    Data = JsonSerializer.Serialize(productModel),
                    TimeStamp = DateTime.Now
                });
                return null;
            }

            return response;
        }
    }
}
