using DataLayer;
using DataLayer.Entities;
using DataLayer.MSSQL;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.CommonServices.Abstraction;
using System;
using System.Collections.Generic;
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
        public FileService()
        {
            OrcusSMEContext context = new OrcusSMEContext(new DbContextOptions<OrcusSMEContext>());
            _fileRepo = new FileRepo(context);
            _crashLogRepo = new CrashLogRepo(context);
        }

        public int[] SaveFiles(string[] FileNames, string[] FilePaths)
        {
            int pk;
            int[] result = new int[FileNames.Length];
            try
            {
                if(_fileRepo.AsQueryable().Count() > 0)
                    pk = _fileRepo.AsQueryable().Max(x => x.FileId) + 1;
                else
                    pk = 1;
                for (int i = 0; i < FileNames.Length; i++)
                {
                    _fileRepo.Add(new File
                    {
                        FileId = i + pk,
                        FileName = FileNames[i],
                        FilePath = FilePaths[i]
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
