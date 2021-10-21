using DataLayer;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.MySql;

namespace Services.Implementation
{
    public class CategorService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        //private readonly IProductRepo _productRepo;
        private readonly ICrashLogRepo _crashLogRepo;
        public CategorService()
        {
            OrcusUMSContext context = new OrcusUMSContext(new DbContextOptions<OrcusUMSContext>());
            _categoryRepo = new CategoryRepo(context);
            _crashLogRepo = new CrashLogRepo(context);
            //_productRepo = new IProductRepo(context);
        }

        public List<DataLayer.Models.Category> AddCategory(DataLayer.Models.Category category)
        {
            try
            {
                int pk = _categoryRepo.AsQueryable().Count() + 1;
                List<DataLayer.Entities.Category> alreadyExists = _categoryRepo.AsQueryable().Where(x => x.OutletId == category.OutletId && x.CategoryName == category.CategoryName).ToList();
                if (alreadyExists != null || alreadyExists.Count() > 0)
                    return new List<DataLayer.Models.Category>();
                else
                {
                    _categoryRepo.Add(new DataLayer.Entities.Category
                    {
                        CategoryId = pk,
                        CategoryName = category.CategoryName,
                        OutletId = category.OutletId,
                        ParentCategoryId = category.ParentCategoryId
                    });

                    return GetCategoriesByOutlets((int)category.OutletId);
                }
            }
            catch (Exception ex)
            {
                _categoryRepo.Rollback();

                int pk = _crashLogRepo.AsQueryable().Count() + 1;

                _crashLogRepo.Add(new DataLayer.Entities.CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "CategoryService",
                    MethodName = "AddCategory",
                    ErrorMessage = ex.Message.ToString(),
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message).ToString(),
                    Data = category.UserId,
                    TimeStamp = DateTime.Now
                });

                return null;
            }
        }

        public bool DeleteCategory(int OutletId)
        {
            try
            {
                DataLayer.Entities.Category category = _categoryRepo.Get(OutletId);
                // Must handle products under this category
                _categoryRepo.Delete(category);

                return true;
            } 
            catch (Exception ex)
            {
                _categoryRepo.Rollback();

                int pk = _crashLogRepo.AsQueryable().Count() + 1;

                _crashLogRepo.Add(new DataLayer.Entities.CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "CategoryService",
                    MethodName = "DeleteCategory",
                    ErrorMessage = ex.Message.ToString(),
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message).ToString(),
                    Data = OutletId.ToString(),
                    TimeStamp = DateTime.Now
                });

                return false;
            }
        }

        public List<DataLayer.Models.Category> GetCategoriesByOutlets(int outletId)
        {
            try
            {
                List<DataLayer.Models.Category> response = new List<DataLayer.Models.Category>();
                response = _categoryRepo.AsQueryable().Where(x => x.OutletId == outletId)
                    .Select(x => new DataLayer.Models.Category {
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        OutletId = x.OutletId,
                        ParentCategoryId = x.ParentCategoryId,
                        UserId = x.Outlet.UserId
                    }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                _categoryRepo.Rollback();

                int pk = _crashLogRepo.AsQueryable().Count() + 1;

                _crashLogRepo.Add(new DataLayer.Entities.CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "CategoryService",
                    MethodName = "GetCategoriesByOutlets",
                    ErrorMessage = ex.Message.ToString(),
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message).ToString(),
                    Data = "",
                    TimeStamp = DateTime.Now
                });

                return null;
            }
        }

        public bool SaveHierarchy(string hierarchy)
        {
            try
            {
                string trimmed = hierarchy.Replace('"', ' ');
                //_categoryRepo.AsQueryable().Where(x => x.)
                return true;
            }
            catch(Exception ex)
            {
                _categoryRepo.Rollback();

                int pk = _crashLogRepo.AsQueryable().Count() + 1;

                _crashLogRepo.Add(new DataLayer.Entities.CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "CategoryService",
                    MethodName = "GetCategoriesByOutlets",
                    ErrorMessage = ex.Message,
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message).ToString(),
                    Data = "",
                    TimeStamp = DateTime.Now
                });

                return false;
            }
        }
    }
}
