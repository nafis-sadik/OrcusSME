using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface ICategoryService
    {
        public List<Category> AddCategory(Category category);
        public List<Category> GetCategoriesByOutlets(int OutletId);
        public bool DeleteCategory(int OutletId);
        public bool SaveHierarchy(string Hierarchy);
    }
}
