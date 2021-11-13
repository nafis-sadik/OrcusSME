using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Orcus.Abstraction
{
    public interface ICategoryService
    {
        public List<CategoryModel> AddCategory(CategoryModel category);
        public List<CategoryModel> GetCategoriesByOutlets(int OutletId);
        public bool DeleteCategory(int OutletId);
        public bool SaveHierarchy(CategoryModel categoryModel);
    }
}
