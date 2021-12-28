using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Orcus.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        [Route("ProductUnitTypes")]
        public IActionResult GetProductUnitTypes() {
            try
            {
                return Ok(new { Response = _productService.GetProductUnitTypes() });
            }
            catch(Exception ex)
            {
                return Conflict(new { Response = CommonConstants.HttpResponseMessages.Exception });
            }
        }

        //[Authorize]
        [HttpPut]
        [Route("ProductUnitTypes")]
        public IActionResult AddProductUnitTypes(ProductUnitTypeModel purchaseModel)
        {
            if (_productService.AddProductUnitTypes(purchaseModel))
                return Ok(new { Response = "Success" });
            else
                return Conflict(new { Response = CommonConstants.HttpResponseMessages.Exception });
        }

        [Authorize]
        [HttpPost]
        [Route("Purchase")]
        public IActionResult Purchase(ProductModel purchaseModel)
        {
            if (_productService.PurchaseProduct(purchaseModel))
                return Ok(new { Response = "Success" });
            else
                return Conflict(new { Response = CommonConstants.HttpResponseMessages.Exception });
        }

        [Authorize]
        [HttpPost]
        [Route("Sell")]
        public IActionResult Sell(ProductModel purchaseModel)
        {
            if (_productService.SellProduct(purchaseModel) == true)
                return Ok(new { Response = "Success" });
            else if (_productService.SellProduct(purchaseModel) == null)
                return Conflict(new { Response = "Product not found" });
            else
                return Conflict(new { Response = CommonConstants.HttpResponseMessages.Exception });
        }

        //[Authorize]
        //[HttpPut]
        //[Route("ProductUnitTypes")]
        //public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        //{
        //    try
        //    {
        //        long size = files.Sum(f => f.Length);

        //        foreach (var formFile in files)
        //        {
        //            if (formFile.Length > 0)
        //            {
        //                var filePath = System.IO.Path.GetTempFileName();

        //                using (var stream = System.IO.File.Create(filePath))
        //                {
        //                    await formFile.CopyToAsync(stream);
        //                }
        //            }
        //        }

        //        // Process uploaded files
        //        // Don't rely on or trust the FileName property without validation.

        //        return Ok(new { count = files.Count, size });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Conflict(ex.Message);
        //    }
        //}
    }
}
