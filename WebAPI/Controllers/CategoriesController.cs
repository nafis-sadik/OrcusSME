using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Orcus.Abstraction;
using System.Collections.Generic;

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
        public IActionResult Add(CategoryModel category)
        {
            List<CategoryModel> response = _categoryService.AddCategory(category);
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
            List<CategoryModel> response = _categoryService.GetCategoriesByOutlets(outletId);
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

        [Authorize]
        [HttpPost]
        [Route("SaveHierarchy")]
        public IActionResult SaveHierarchy(CategoryModel categoryModel)
        {
            if (_categoryService.SaveHierarchy(categoryModel))
                return new OkObjectResult(new { Response = "Success" });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }
    }
}
