using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Models;

namespace ShoeperStar.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private AppDbContext _dbContext;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
