using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Abstraction;
using Models;
using DataLayer;

namespace Application.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController
    {
        private readonly IUserService _userServices;
        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("LogIn")]
        public IActionResult LogIn(UserModel user)
        {
            bool? logInResponse = _userServices.LogIn(user.UserName, user.Password, out string token, out string userId);

            if (logInResponse == true)
                return new OkObjectResult(new { Response = token, UserId = userId });
            else if (logInResponse == false)
                return new ConflictObjectResult(new { Response = CommonConstants.HttpResponseMessages.PasswordMismatched });
            else
                return new ConflictObjectResult(new { Response = CommonConstants.HttpResponseMessages.UserNotFound });
        }

        [HttpPut]
        [Route("SignUp")]
        public IActionResult SignUp(UserModel user)
        {
            bool? signUpResponse = _userServices.SignUp(user, out string token, out string userId);
            if (signUpResponse == true)
                return new OkObjectResult(new { Response = token, UserId = userId });
            else if (signUpResponse == null)
                return new ConflictObjectResult(new { Response = token });
            else
                return new ConflictObjectResult(new { Response = "Internal Error" });
        }

        [HttpGet]
        [Route("Archive/{id}")]
        public IActionResult ArchiveAccount(string id)
        {
            if (_userServices.ArchiveAccount(id))
                return new OkObjectResult(new { Response = "Success" });
            else
                return new ConflictObjectResult(new { Response = "Internal Error" });
        }

        [HttpGet]
        [Route("PermaDelete/{id}")]
        public IActionResult PermanantDelete(string id)
        {
            if (_userServices.DeleteAccount(id))
                return new OkObjectResult(new { Response = "Success" });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        [HttpGet]
        [Route("ResetPassword/{id}")]
        public IActionResult ResetPassword(string id)
        {
            if (_userServices.ResetPassword(id))
                return new OkObjectResult(new { Response = "Success" });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }
    }
}
