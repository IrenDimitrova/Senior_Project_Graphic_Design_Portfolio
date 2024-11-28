using Microsoft.AspNetCore.Mvc;

namespace Senior_Project_Graphic_Design_Portfolio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["CurrentPage"] = "Home";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}