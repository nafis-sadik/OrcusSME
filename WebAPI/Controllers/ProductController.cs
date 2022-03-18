using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Services.Orcus.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Authorize]
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
        [Route("SaveProduct")]
        public IActionResult AddProduct(ProductModel purchaseModel)
        {
            var productId = _productService.SaveProduct(purchaseModel);
            if (productId != null)
                return Ok(new { Response = productId });
            else
                return Conflict(new { Response = CommonConstants.HttpResponseMessages.Exception });
        }

        [Authorize]
        [HttpPost]
        [Route("Purchase")]
        public IActionResult PurchaseProduct(ProductModel purchaseModel)
        {
            bool productId = _productService.PurchaseProduct(purchaseModel.ProductId, purchaseModel.Quantity, purchaseModel.PurchasingPrice);
            if (productId)
                return Ok(new { Response = productId });
            else
                return Conflict(new { Response = CommonConstants.HttpResponseMessages.Exception });
        }

        [Authorize]
        [HttpPost]
        [Route("Sell")]
        public IActionResult Sell(List<ProductModel> productModels)
        {
            foreach(ProductModel productModel in productModels)
            {
                if (_productService.SellProduct(productModel) == true)
                    continue;
                else if (_productService.SellProduct(productModel) == null)
                    continue;
                else
                    return Conflict(new { Response = CommonConstants.HttpResponseMessages.Exception });
            }
            return Ok(new { Response = "Success" });
        }

        [Authorize]
        [HttpPost]
        [Route("Inventory")]
        public IActionResult GetInventory(OutletModel outletModel)
        {
            IEnumerable<ProductModel> inventory = _productService.GetInventory(outletModel);
            if (inventory != null)
            {
                return Ok(new { Response = inventory });
            }
            else
                return Conflict(new { Response = CommonConstants.HttpResponseMessages.Exception });
        }

        [Authorize]
        [HttpDelete]
        [Route("ArchiveProduct")]
        public IActionResult ArchiveProduct()
        {
            try
            {
                string requestBodyData;
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    requestBodyData = reader.ReadLineAsync().Result;
                    if (string.IsNullOrEmpty(requestBodyData))
                        return BadRequest(CommonConstants.HttpResponseMessages.InvalidInput);
                    ProductModel requestBodyObject = JsonConvert.DeserializeObject<ProductModel>(requestBodyData);

                    bool? response = _productService.ArchiveProduct(requestBodyObject.UserId, requestBodyObject.ProductId);
                    if (response == true)
                        return Ok(new { Response = "Success" });
                    else if (response == null)
                        return Conflict(new { Response = CommonConstants.HttpResponseMessages.InvalidInput });
                    else
                        return Conflict(new { Response = CommonConstants.HttpResponseMessages.Exception });
                }

                return Ok(requestBodyData);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetProduct")]
        public IActionResult GetProduct(int productId)
        {
            try
            {
                ProductModel productModel = _productService.GetProductById(productId);
                return Ok(productModel);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
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
