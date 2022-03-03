using DataLayer;
using DataLayer.Entities;
using DataLayer.MSSQL;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.CommonServices.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.CommonServices.Implementation
{
    public class FileService : IFileService
    {
        private readonly IFileRepo _fileRepo;
        private readonly ICrashLogRepo _crashLogRepo;
        private readonly IProductPictureRepo _productPictureRepo;
        public FileService()
        {
            OrcusSMEContext context = new OrcusSMEContext(new DbContextOptions<OrcusSMEContext>());
            _fileRepo = new FileRepo(context);
            _crashLogRepo = new CrashLogRepo(context);
            _productPictureRepo = new ProductPictureRepo(context);
        }

        public int[] SaveProductImages(string[] FileNames, int productId)
        {
            int pk;
            int[] result = new int[FileNames.Length];
            try
            {
                if(_fileRepo.AsQueryable().Any())
                    pk = _fileRepo.AsQueryable().Max(x => x.FileId) + 1;
                else
                    pk = 1;

                int productPicPk = 0;
                if (_productPictureRepo.AsQueryable().Any())
                    productPicPk = _productPictureRepo.AsQueryable().Max(x => x.ProductPictureId) + 1;
                else
                    productPicPk = 1;

                for (int i = 0; i < FileNames.Length; i++)
                {
                    var file = new DataLayer.Entities.File
                    {
                        FileId = i + pk,
                        FileName = FileNames[i],
                        FilePath = Path.Combine("Images", FileNames[i])
                    };
                    _fileRepo.Add(file);

                    
                    _productPictureRepo.Add(new ProductPicture
                    {
                        FileId = file.FileId,
                        ProductId = productId
                    });

                    result[i] = i + pk;
                }
                return result;
            }
            catch (Exception ex)
            {
                _fileRepo.Rollback();

                if (!_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.GetMaxPK("CrashLogId") + 1;

                string msg = (string.IsNullOrEmpty(ex.Message) || ex.Message.ToLower().Contains(CommonConstants.MsgInInnerException.ToLower()))
                            ? ex.InnerException.Message
                            : ex.Message;
                _crashLogRepo.Add(new Crashlog
                {
                    CrashLogId = pk,
                    ClassName = "FileService",
                    MethodName = "SaveFiles",
                    ErrorMessage = ex.Message,
                    ErrorInner = msg,
                    Data = JsonSerializer.Serialize("Failed to save file"),
                    TimeStamp = DateTime.Now
                });
                return null;
            }
        }
    }
}
