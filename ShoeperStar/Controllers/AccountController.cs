using Microsoft.AspNetCore.Mvc;

namespace ShoeperStar.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
