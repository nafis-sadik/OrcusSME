using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Services.Abstraction;

namespace OrcusUMS.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult SignIn() => View();
        [HttpGet]
        public IActionResult SignUp() => View();
        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public IActionResult SignIn(string userName, string pass)
        {
            string response = _userService.LogIn(userName, pass);
            if (!string.IsNullOrEmpty(response))
                return Ok(response);
<<<<<<< HEAD
                // return RedirectToAction("Index", "Dashboard");
=======
>>>>>>> 06e6058c31ad8980cb1053f95a545e1a05a6aebb
            else if (response == null)
                return Conflict(CommonConstants.HttpResponseMessages.UserNotFound);
            else
                return Conflict(CommonConstants.HttpResponseMessages.PasswordMismatched);
        }
        [HttpPost]
        public IActionResult SignUp(UserModel user)
        {
            if (_userService.SignUp(user))
                // Session not kept yet
                return RedirectToAction("Index", "Dashboard");
            else
                return View(model: "User Registration Failed! Please try again.");
        }
        public IActionResult LogOut()
        {
            return RedirectToAction("SignIn", "User");
        }
    }
}
