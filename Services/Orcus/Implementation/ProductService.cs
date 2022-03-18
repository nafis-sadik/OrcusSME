using DataLayer;
using DataLayer.Entities;
using DataLayer.Models;
using DataLayer.MSSQL;
using DataLayer.MySql;
using Microsoft.EntityFrameworkCore;
using Models;
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
        //private readonly IProductPictureRepo _productPicRepo;

        public ProductService()
        {
            OrcusSMEContext context = new OrcusSMEContext(new DbContextOptions<OrcusSMEContext>());
            _productUnitTypeRepo = new ProductUnitTypeRepo(context);
            _crashLogRepo = new CrashLogRepo(context);
            _inventoryLogRepo = new InventoryLogRepo(context);
            _productRepo = new ProductRepo(context);
            _outletManagerRepo = new OutletManagerRepo(context);
            _categoryRepo = new CategoryRepo(context);
            //_productPicRepo = new ProductPictureRepo(context);
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

        public int? SaveProduct(ProductModel product)
        {
            Product productData;
            try
            {
                if (product.ProductId <= 0)
                    productData = new Product();
                else
                    productData = _productRepo.Get(product.ProductId);

                if(productData == null)
                    return null;

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
                if (product.ProductId <= 0)
                    _productRepo.Add(productData);
                else
                    _productRepo.Update(productData);

                return productData.ProductId;
            }
            catch (Exception ex)
            {
                _productUnitTypeRepo.Rollback();
                _productRepo.Rollback();

                _crashLogRepo.Add(new Crashlog
                {
                    ClassName = "ProductService",
                    MethodName = "SaveProduct",
                    ErrorMessage = ex.Message,
                    ErrorInner = ex.InnerException == null ? "" : ex.InnerException.Message,
                    Data = JsonSerializer.Serialize(product),
                    TimeStamp = DateTime.Now
                });

                return null;
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

                _inventoryLogRepo.Add(new InventoryLog
                {
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

        public IEnumerable<ProductModel> GetInventory(OutletModel outletModel)
        {
            IEnumerable<ProductModel> response = new List<ProductModel>();
            try
            {
                List<int> outlets = new List<int>();
                // Return all outlets when no outlet selected
                if (outletModel.OutletId <= 0)
                {
                    // Get Outlet Ids of Person
                    outlets.AddRange(_outletManagerRepo.AsQueryable()
                        .Where(x => x.UserId == outletModel.UserId && x.Status == CommonConstants.StatusTypes.Active)
                        .Select(x => x.OutletId)
                        .ToList());
                }
                else
                {
                    // Check if the person owns the outlet or not
                    Outlet outlet = _outletManagerRepo.Get(outletModel.OutletId);
                    if (outlet == null || outlet.UserId != outletModel.UserId)
                        return null;

                    outlets.Add((int)outletModel.OutletId);
                }


                return _productRepo.AsQueryable()
                    .Where(product => outlets.Contains(product.Category.OutletId) && product.Status == CommonConstants.StatusTypes.Active)
                    .Select(product => new ProductModel
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Quantity = product.Quantity,
                        RetailPrice = (int)product.RetailPrice,
                        OutletName = product.Category.Outlet.OutletName
                    })
                    .Skip(outletModel.Skip)
                    .Take(outletModel.PageSize)
                    .ToList();
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
                    ClassName = "ProductService",
                    MethodName = "SellProduct",
                    ErrorMessage = ex.Message,
                    ErrorInner = msg,
                    Data = JsonSerializer.Serialize("string userId = " + outletModel.UserId + ", int? outletId = " + outletModel.OutletId),
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
                string prductOwnerId = _outletManagerRepo.Get(_categoryRepo.AsQueryable().FirstOrDefault(x => x.CategoryId == _productRepo.Get(productId).CategoryId).OutletId).UserId;
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

        public bool PurchaseProduct(int productId, int quantity, double purchasingPrice)
        {
            try
            {
                Product product = _productRepo.Get(productId);
                if (product == null) return false;
                product.Quantity += quantity;
                _productRepo.Update(product);

                _inventoryLogRepo.Add(new InventoryLog
                {
                    ActivityDate = DateTime.Now,
                    InventoryUpdateType = CommonConstants.ActivityTypes.Purchase,
                    Price = purchasingPrice,
                    ProductId = product.ProductId,
                    Quantity = quantity,
                });

                return true;
            }
            catch (Exception ex)
            {
                _productUnitTypeRepo.Rollback();
                _productRepo.Rollback();

                _crashLogRepo.Add(new Crashlog
                {
                    ClassName = "ProductService",
                    MethodName = "PurchaseProduct",
                    ErrorMessage = ex.Message,
                    ErrorInner = ex.InnerException == null? "" : ex.InnerException.Message,
                    Data = JsonSerializer.Serialize("ProductId <=> " + productId + " Quantity <=> " + quantity + " Purchasing Price <=> " + purchasingPrice),
                    TimeStamp = DateTime.Now
                });

                return false;
            }
        }

        public ProductModel GetProductById(int productId)
        {
            Product product =_productRepo.Get(productId);
            ProductModel productData = new ProductModel();
            productData.ProductName = product.ProductName;
            productData.CategoryId = product.CategoryId;
            productData.SubCategoryId = (int)_categoryRepo.AsQueryable().Where(c => c.CategoryId == productId).FirstOrDefault().ParentCategoryId;
            productData.ProductDescription = product.Description;
            productData.ShortDescription = product.ShortDescription;
            productData.RetailPrice = (int)product.RetailPrice;
            productData.Quantity += product.Quantity;
            productData.ShortDescription = product.ShortDescription;
            productData.ProductSpecs = product.Specifications;
            productData.UnitTypeId = product.UnitTypeId;
            return productData;
        }
    }
}
