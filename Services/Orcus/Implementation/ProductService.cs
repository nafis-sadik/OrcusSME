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
        private readonly ICategoryRepo _categoryRepo;
        private readonly IProductPictureRepo _productPicRepo;

        public ProductService()
        {
            OrcusSMEContext context = new OrcusSMEContext(new DbContextOptions<OrcusSMEContext>());
            _productUnitTypeRepo = new ProductUnitTypeRepo(context);
            _crashLogRepo = new CrashLogRepo(context);
            _inventoryLogRepo = new InventoryLogRepo(context);
            _productRepo = new ProductRepo(context);
            _outletManagerRepo = new OutletManagerRepo(context);
            _categoryRepo = new CategoryRepo(context);
            _productPicRepo = new ProductPictureRepo(context);
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
                productData.RetailPrice = product.RetailPrice;
                productData.Quantity += product.Quantity;
                productData.ShortDescription = product.ShortDescription;
                productData.Specifications = product.ProductSpecs;
                productData.UnitTypeId = product.UnitTypeId;
                productData.Status = CommonConstants.StatusTypes.Active;

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
                    Quantity = (int)productData.Quantity,
                });

                foreach(int id in product.ProductImageIds)
                {
                    _productPicRepo.Add(new ProductPicture
                    {
                        ProductPictureId = pk,
                        FileId = id,
                    });
                }

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
                    Quantity = (int)productData.Quantity,
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

        public IEnumerable<ProductModel> GetInventory(string userId, int? outletId)
        {
            IEnumerable<ProductModel> response = new List<ProductModel>();
            try
            {
                // Return null if UserId is null or empty
                if (string.IsNullOrEmpty(userId))
                    return null;

                List<Product> products = new List<Product>();
                List<ProductModel> productsList = new List<ProductModel>();
                // Return all outlets when no outlet selected
                if (outletId <= 0 || outletId == null)
                {
                    // Get Outlet Ids of Person
                    var outlets = _outletManagerRepo.AsQueryable()
                        .Where(x => x.UserId == userId && x.Status == CommonConstants.StatusTypes.Active)
                        .ToList();
                    foreach (Outlet outlet in outlets)
                        productsList.AddRange(_productRepo.AsQueryable()
                            .Where(product => product.Category.OutletId == outlet.OutletId && product.Status == CommonConstants.StatusTypes.Active)
                            .Select(product => new ProductModel
                            {
                                ProductId = product.ProductId,
                                ProductName = product.ProductName,
                                Quantity = (int)product.Quantity,
                                PurchasingPrice = product.PurchasingPrice,
                                RetailPrice = (int)product.RetailPrice,
                                OutletName = product.Category.Outlet.OutletName
                            })
                            .ToList());
                }
                else
                {
                    // Check if the person owns the outlet or not
                    Outlet outlet = _outletManagerRepo.Get(Convert.ToDecimal(outletId));
                    if (outlet.UserId != userId)
                        return null;

                    // Get all products of the outlet
                    productsList.AddRange(_productRepo.AsQueryable()
                        .Where(x => x.Category.OutletId == outletId)
                        .Select(product => new ProductModel
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            Quantity = (int)product.Quantity,
                            PurchasingPrice = product.PurchasingPrice,
                            RetailPrice = (int)product.RetailPrice,
                            OutletName = product.Category.Outlet.OutletName
                        })
                        .ToList());
                }

                response = productsList;
            }
            catch (Exception ex)
            {
                int pk;
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
                    Data = JsonSerializer.Serialize("string userId = " + userId + ", int? outletId = " + outletId),
                    TimeStamp = DateTime.Now
                });
                return null;
            }

            return response;
        }

        public bool? ArchiveProduct(string userId, int productId)
        {
            try
            {
                // Check if product exists
                Product Product = _productRepo.Get(productId);
                if (Product == null)
                    return false;

                // Veridy user's product
                string prductOwnerId = _outletManagerRepo.Get((decimal)_categoryRepo.AsQueryable().FirstOrDefault(x => x.CategoryId == _productRepo.Get(productId).CategoryId).OutletId).UserId;
                if (prductOwnerId != userId)
                    return false;

                // Archive the product
                Product.Status = CommonConstants.StatusTypes.Archived;
                _productRepo.Update(Product);
                return true;
            }
            catch (Exception ex)
            {
                int pk;
                _productRepo.Rollback();

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
                    MethodName = "ArchiveProduct",
                    ErrorMessage = ex.Message,
                    ErrorInner = msg,
                    Data = JsonSerializer.Serialize("string userId = " + userId + ", int? productId = " + productId),
                    TimeStamp = DateTime.Now
                });

                return null;
            }
        }
    }
}
