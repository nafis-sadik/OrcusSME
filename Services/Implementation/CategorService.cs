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

        public bool AddCategory(DataLayer.Models.Category category)
        {
            try
            {
                int pk = _categoryRepo.AsQueryable().Count() + 1;
                _categoryRepo.Add(new Entities.Category
                {
                    CategoryId = pk,
                    CategoryName = category.CategoryName,
                    OutletId = category.OutletId,
                    ParentCategoryId = category.ParentCategoryId
                });

                return true;
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

                return false;
            }
        }
    }
}
