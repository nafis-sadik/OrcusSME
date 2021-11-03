using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoriesController
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize]
        [HttpPut]
        [Route("Add")]
        public IActionResult Add(Category category)
        {
            List<Category> response = _categoryService.AddCategory(category);
            if (response != null)
                return new OkObjectResult(new { Response = response });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        [Authorize]
        [HttpGet]
        [Route("GetCategoriesOfOutlets/{outletId}")]
        public IActionResult GetCategoriesOfOutlets(int outletId)
        {
            List<Category> response = _categoryService.GetCategoriesByOutlets(outletId);
            if (response != null)
                return new OkObjectResult(new { Response = response });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete/{categoryId}")]
        public IActionResult Delete(int categoryId)
        {
            if (_categoryService.DeleteCategory(categoryId))
                return new OkObjectResult(new { Response = "Success" });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        //[Authorize]
        [HttpGet]
        [Route("SaveHierarchy/{saveHierarchy}")]
        public IActionResult SaveHierarchy(string saveHierarchy)
        {
            if (_categoryService.SaveHierarchy(saveHierarchy))
                return new OkObjectResult(new { Response = "Success" });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }
    }
}
