using DBMS.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMS.Services.Abstraction;
using UMS.Services.Implementation;
using WebInstaller.Model;

namespace WebInstaller.Controllers
{
    [Route("api/Database")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseInstallerService _databaseInstallerService;
        private readonly IUserService _userService;
        public DatabaseController(IUserService userService)
        {
            _databaseInstallerService = new DBMS.Services.Implementation.MySqlDatabaseInstaller();
            _userService = userService;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(DatabaseInstallConfig config)
        {
            bool? response = _databaseInstallerService.InstallDatabase(config.SelectedDatabase);
            if (response == true)
            {
                _userService.SignUp(new Models.UserModel
                {
                    DefaultEmail = config.EmailAddress,
                    AccountBalance = 99999,
                    FirstName = "Admin",
                    MiddleName = "Admin",
                    LastName = "Admin",
                    Password = "Orcus@1234",
                    UserName = "admin",
                    Status = "A"
                }, out string token, out string userId);
                return Ok("Success");
            }
            else if (response == null)
                return Conflict("Database not supported yet");
            else
                return Conflict("Failed to create database");
        }
    }
}
