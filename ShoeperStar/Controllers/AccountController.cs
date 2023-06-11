using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Models;
using ShoeperStar.Models.ViewModels;

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

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user == null)
            {
                TempData["Error"] = "Email address is not registered. Please, try again!";
                return View(loginVM);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (!isPasswordValid)
            {
                TempData["Error"] = "Password is incorrect. Please, try again!";
                return View(loginVM);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, isPersistent: false, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
            {
                TempData["Error"] = "An error occurred during login attempt. Please, try again!";
                return View(loginVM);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
