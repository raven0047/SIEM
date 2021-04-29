using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            string pass = "111222";
            string login = "admin";

            _logger.LogError("Login");

            if (model.Login == login && model.Password == pass) return RedirectPermanent("~/Home/AuthorizedMenu");
            else return RedirectPermanent("~/Home/BadLogin");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult BadLogin()
        {
            return View("AccessDenied");
        }

        public IActionResult AuthorizedMenu()
        {
            return View("AccessPassedView");
        }
    }
}
