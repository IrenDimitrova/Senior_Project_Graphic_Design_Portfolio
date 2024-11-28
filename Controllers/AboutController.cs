using Microsoft.AspNetCore.Mvc;

namespace Senior_Project_Graphic_Design_Portfolio.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            ViewData["CurrentPage"] = "About";
            return View();
        }
    }
}
