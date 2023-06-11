using Microsoft.AspNetCore.Mvc;

namespace ShoeperStar.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
