using DataLayer;
using DataLayer.Entities;
using DataLayer.Models;
using DataLayer.MySql;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Orcus.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Orcus.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductUnitTypeRepo _productUnitTypeRepo;
        private readonly ICrashLogRepo _crashLogRepo;
        public ProductService()
        {
            OrcusUMSContext context = new OrcusUMSContext(new DbContextOptions<OrcusUMSContext>());
            _productUnitTypeRepo = new ProductUnitTypeRepo(context);
            _crashLogRepo = new CrashLogRepo(context);
        }

        public IEnumerable<ProductUnitTypeModel> GetProductUnitTypes()
        {
            return _productUnitTypeRepo.GetAll().Select(x =>
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
                    UnitTypeIds = productUnitType.UnitTypeId,
                    UnitTypeNames = productUnitType.UnitTypeName
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
                    _crashLogRepo.Add(new CrashLog
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
    }
}
