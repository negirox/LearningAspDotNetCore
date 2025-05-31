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
        public IActionResult UserLogin(IFormCollection form)
        {
            if (form.TryGetValue("Username", out StringValues userNameValues) &&
                form.TryGetValue("Password", out StringValues passwordValues))
            {
                string userName = userNameValues.ToString();
                string password = passwordValues.ToString();

                // Pass these values to the database and check whether the user and password exist.  
                UserService userService = new UserService();
                bool isValid = userService.LoginUser(userName, password);

                ViewData["Message"] = isValid ? "Success..." : "Failed...";
            }
            else
            {
                ViewData["Message"] = "Failed...";
            }

            return View();
        }
    }
}
