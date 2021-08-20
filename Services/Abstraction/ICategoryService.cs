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
        public bool AddCategory(Category category);
        public List<Category> GetCategoriesByOutlets(int OutletId);
    }
}
