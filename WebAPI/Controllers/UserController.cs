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
    //[ApiController]
    public class UserController : ControllerBase
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
            bool? logInResponse = _userServices.LogIn(user.UserName, user.Password, out string token);

            if (logInResponse == true)
                return Ok(token);
            else if (logInResponse == false)
                return Conflict(CommonConstants.HttpResponseMessages.PasswordMismatched);
            else
                return Conflict(CommonConstants.HttpResponseMessages.UserNotFound);
        }

        [HttpPut]
        [Route("SignUp")]
        public IActionResult SignUp(UserModel user)
        {
            bool? signUpResponse = _userServices.SignUp(user, out string token);
            if (signUpResponse == true)
                return Ok(token);
            else if (signUpResponse == null)
                return Conflict(token);
            else
                return Conflict("Internal Error");
        }

        [HttpGet]
        [Route("Archive/{id}")]
        public IActionResult ArchiveAccount(string id)
        {
            if (_userServices.ArchiveAccount(id))
                return Ok();
            else
                return Conflict();
        }

        [HttpGet]
        [Route("PermaDelete/{id}")]
        public IActionResult PermanantDelete(string id)
        {
            if (_userServices.DeleteAccount(id))
                return Ok();
            else
                return Conflict();
        }

        [HttpGet]
        [Route("ResetPassword/{id}")]
        public IActionResult ResetPassword(string id)
        {
            if (_userServices.ResetPassword(id))
                return Ok();
            else
                return Conflict();
        }
    }
}
