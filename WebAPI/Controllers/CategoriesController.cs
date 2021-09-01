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
        [Route("GetCategoriesOfOutlets/{OutletId}")]
        public IActionResult GetCategoriesOfOutlets(int OutletId)
        {
            List<Category> response = _categoryService.GetCategoriesByOutlets(OutletId);
            if (response != null)
                return new OkObjectResult(new { Response = response });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete/{CategoryId}")]
        public IActionResult Delete(int CategoryId)
        {
            if (_categoryService.DeleteCategory(CategoryId))
                return new OkObjectResult(new { Response = "Success" });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        //[Authorize]
        [HttpGet]
        [Route("SaveHierarchy/{SaveHierarchy}")]
        public IActionResult SaveHierarchy(string SaveHierarchy)
        {
            if (_categoryService.SaveHierarchy(SaveHierarchy))
                return new OkObjectResult(new { Response = "Success" });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }
    }
}
