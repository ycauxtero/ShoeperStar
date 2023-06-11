using Microsoft.AspNetCore.Mvc;

namespace ShoeperStar.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
