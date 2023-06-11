using Microsoft.AspNetCore.Mvc;

namespace ShoeperStar.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
