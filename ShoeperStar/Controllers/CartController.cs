using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;
using System.Security.Claims;

namespace ShoeperStar.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;


        public CartController(IRepositoryManager repositoryManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddItemToCart(int shoeSizeId)
        {
            var userId = GetLoggedInUserId();
            await _repositoryManager.CartItems.AddCartItem(shoeSizeId, userId);

            await _repositoryManager.SaveAsync();

            return RedirectToAction("Filter", "Home");
        }









        private string GetLoggedInUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
