using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Services.CommonServices.Abstraction;

namespace WebAPI.Controllers
{
    [Route("api/Files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [Authorize]
        [HttpPost]
        [Route("UploadFiles")]
        public async Task<IActionResult> UploadImages()
        {
            IFormFileCollection DocumentPhotos = Request.Form.Files;
            try
            {
                if (DocumentPhotos == null || !DocumentPhotos.Any())
                    return NotFound(new int[] { });

                string[] FileNames = new string[DocumentPhotos.Count];
                string uploads = Path.Combine("Images");
                int productId = int.Parse(Request.Headers["ProductId"]);
                for (int i = 0; i < DocumentPhotos.Count(); i++)
                {
                    if (DocumentPhotos[i].Length > 0)
                    {
                        string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        string fileName = timeStamp + DocumentPhotos[i].FileName;
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            await DocumentPhotos[i].CopyToAsync(fileStream);
                        }
                        FileNames[i] = fileName;
                    }
                }
                if (_fileService.SaveProductImages(FileNames, productId).Any())
                    return Ok(new { Response = "Success" });
                else
                    return Conflict(new { Response = "Error" });
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

    }
}
