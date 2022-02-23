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
        [HttpPost]
        [Route("UploadFiles")]
        public async Task<IActionResult> UploadImages(IFormFileCollection DocumentPhotos)
        {
            try
            {
                if (DocumentPhotos == null || !DocumentPhotos.Any())
                {
                    DocumentPhotos = Request.Form.Files;
                    if (DocumentPhotos == null || !DocumentPhotos.Any())
                        return NotFound("No files sent to save");
                }
                string[] FileNames = new string[DocumentPhotos.Count];
                string[] FilePaths = new string[DocumentPhotos.Count];
                var uploads = Path.Combine("Images");

                for (int i = 0; i < DocumentPhotos.Count(); i++)
                {
                    if (DocumentPhotos[i].Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, DocumentPhotos[i].FileName), FileMode.Create))
                        {
                            await DocumentPhotos[i].CopyToAsync(fileStream);
                        }
                        FileNames[i] = DocumentPhotos[i].FileName;
                        FilePaths[i] = Path.Combine(uploads, DocumentPhotos[i].FileName);
                    }
                }
                _fileService.SaveFiles(FileNames, FilePaths);
                return Ok( new { Response = "Success" } );
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

    }
}
