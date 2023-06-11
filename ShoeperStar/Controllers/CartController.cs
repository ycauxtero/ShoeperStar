using Microsoft.AspNetCore.Mvc;

namespace ShoeperStar.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
