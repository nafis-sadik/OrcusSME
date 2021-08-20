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
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Category category)
        {
            if (_categoryService.AddCategory(category))
                return new OkObjectResult(new { Response = "Success" });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        //[Authorize]
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
    }
}
