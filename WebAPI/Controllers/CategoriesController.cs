using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Category category)
        {
            return new OkObjectResult(new { Response = "" });
        }
    }
}
