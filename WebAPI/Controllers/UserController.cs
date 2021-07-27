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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;
        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("LogIn/{userName}/{pass}")]
        public IActionResult LogIn(string userName, string pass)
        {
            bool? logInResponse = _userServices.LogIn(userName, pass, out string token);

            if (logInResponse == true)
                return Ok(token);
            else if (logInResponse == false)
                return Conflict(CommonConstants.HttpResponseMessages.PasswordMismatched);
            else
                return Conflict(CommonConstants.HttpResponseMessages.UserNotFound);
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(UserModel user)
        {
            bool? signUpResponse = _userServices.SignUp(user, out string token);
            if (signUpResponse == true)
                return Ok(token);
            else if (signUpResponse == null)
                return Conflict(CommonConstants.HttpResponseMessages.UserExists);
            else
                return Conflict(token);
        }
    }
}
