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
            bool? operation = _userService.LogIn(userName, pass);
            if (operation == true)
                return RedirectToAction("Index", "Home");
            else if (operation == null)
                return View(model: "User Not Foud!");
            else
                return View(model: "Password Was Wrong!");
        }
        [HttpPost]
        public IActionResult SignUp(UserModel user)
        {
            if (_userService.SignUp(user))
                // Session not kept yet
                return RedirectToAction("Index", "Home");
            else
                return View(model: "User Registration Failed! Please try again.");
        }
        public IActionResult LogOut()
        {
            return RedirectToAction("SignIn", "User");
        }
    }
}
