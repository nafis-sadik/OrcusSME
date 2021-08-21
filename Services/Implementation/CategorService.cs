using DataLayer;
using DataLayer.Models;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class CategorService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly ICrashLogRepo _crashLogRepo;
        public CategorService()
        {
            OrcusUMSContext context = new OrcusUMSContext(new DbContextOptions<OrcusUMSContext>());
            _categoryRepo = new CategoryRepo(context);
            _crashLogRepo = new CrashLogRepo(context);
        }

        public List<DataLayer.Models.Category> AddCategory(DataLayer.Models.Category category)
        {
            try
            {
                int pk = _categoryRepo.AsQueryable().Count() + 1;
                List<DataLayer.Models.Category> AlreadyExists = _categoryRepo.AsQueryable().Where(x => x.OutletId == category.OutletId && x.CategoryName == category.CategoryName) != null ? new List<DataLayer.Models.Category>() : null;
                if (AlreadyExists != null)
                    return AlreadyExists;
                _categoryRepo.Add(new Entities.Category
                {
                    CategoryId = pk,
                    CategoryName = category.CategoryName,
                    OutletId = category.OutletId,
                    ParentCategoryId = category.ParentCategoryId
                });

                return GetCategoriesByOutlets((int)category.OutletId);
            }
            catch (Exception ex)
            {
                _categoryRepo.Rollback();

                int pk = _crashLogRepo.AsQueryable().Count() + 1;

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "CategorService",
                    MethodName = "AddCategory",
                    ErrorMessage = ex.Message.ToString(),
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message).ToString(),
                    Data = category.UserId.ToString(),
                    TimeStamp = DateTime.Now
                });

                return null;
            }
        }

        public List<DataLayer.Models.Category> GetCategoriesByOutlets(int OutletId)
        {
            try
            {
                List<DataLayer.Models.Category> response = new List<DataLayer.Models.Category>();
                response = _categoryRepo.AsQueryable().Where(x => x.OutletId == OutletId)
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

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "CategorService",
                    MethodName = "GetCategoriesByOutlets",
                    ErrorMessage = ex.Message.ToString(),
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message).ToString(),
                    Data = "",
                    TimeStamp = DateTime.Now
                });

                return null;
            }
        }
    }
}
