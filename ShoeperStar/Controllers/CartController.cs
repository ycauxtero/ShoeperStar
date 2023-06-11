using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;
using ShoeperStar.Models.ViewModels;
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
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = await _repositoryManager.CartItems.GetCartItemsBasedOnActualStocks(userId);
            var cartItemsVM = _mapper.Map<IEnumerable<CartItemVM>>(cartItems);

            return View(cartItemsVM);
        }

        public async Task<IActionResult> AddItemToCart(int shoeSizeId)
        {
            var userId = GetLoggedInUserId();
            await _repositoryManager.CartItems.AddCartItem(shoeSizeId, userId);

            await _repositoryManager.SaveAsync();

            return RedirectToAction("Filter", "Home");
        }

        public async Task<IActionResult> RemoveItemFromCart(int id)
        {
            var userId = GetLoggedInUserId();

            var cartItems = await _repositoryManager.CartItems.GetCartItems(userId);
            var cartItemToRemove = cartItems.FirstOrDefault(x => x.Id == id);

            if (cartItemToRemove == null)
            {
                return RedirectToAction(nameof(Index));
            }

            await _repositoryManager.CartItems.DeleteCartItem(cartItemToRemove);
            await _repositoryManager.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> IncrementItemQty(int shoeSizeId)
        {
            var userId = GetLoggedInUserId();
            await _repositoryManager.CartItems.AddCartItem(shoeSizeId, userId);

            await _repositoryManager.SaveAsync();

            return RedirectToAction(nameof(Index));
        }








        private string GetLoggedInUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
