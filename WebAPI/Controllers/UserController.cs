using Microsoft.AspNetCore.Mvc;
using UMS.Services.Abstraction;
using Models;
using DataLayer;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController: ControllerBase
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
            bool? logInResponse = _userServices.LogIn(user, out string token, out string userId);

            if (logInResponse == true)
                return new OkObjectResult(new { Response = token, UserId = userId });
            else if (logInResponse == false)
                return new ConflictObjectResult(new { Response = CommonConstants.HttpResponseMessages.PasswordMismatched });
            else
                return new ConflictObjectResult(new { Response = CommonConstants.HttpResponseMessages.UserNotFound });
        }

        [HttpPut]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp()
        {
            UserModel user;
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string body = await reader.ReadToEndAsync();
                user = JsonSerializer.Deserialize<UserModel>(body);
            }

            bool? signUpResponse = _userServices.SignUp(user, out string token, out string userId);
            if (signUpResponse == true)
                return new OkObjectResult(new { Response = token, UserId = userId });
            else if (signUpResponse == null)
                return new ConflictObjectResult(new { Response = token });
            else
                return new ConflictObjectResult(new { Response = CommonConstants.HttpResponseMessages.Exception });
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
