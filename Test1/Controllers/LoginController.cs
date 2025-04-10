using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Test1.Services;

namespace Test1.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(IFormCollection form) {
            string userName = form["Username"];
            string password = form["Password"];
            //pass this va;ue tp database and check whether the user and password is there.
            UserService userService = new UserService();
            bool iaValid = userService.LoginUser(userName, password);
            if (iaValid)
                ViewData["Message"] = "Success...";
            else
                ViewData["Message"] = "Failed...";
                return View();
        }
    }
}
