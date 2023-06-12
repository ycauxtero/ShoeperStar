using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;
using ShoeperStar.Models.ViewModels;
using System.Security.Claims;
using EmailService;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;

namespace ShoeperStar.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(IRepositoryManager repositoryManager, UserManager<AppUser> userManager, 
                                IMapper mapper, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<IActionResult> DecrementItemQty(int shoeSizeId)
        {
            var userId = GetLoggedInUserId();

            var cartItem = new CartItem
            {
                Size = await _repositoryManager.Sizes.GetSizeAsync(shoeSizeId, trackChanges: true),
                UserId = userId
            };

            await _repositoryManager.CartItems.DeleteCartItem(cartItem);
            await _repositoryManager.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Checkout(string total)
        {
            var userId = GetLoggedInUserId();
            var cart = await _repositoryManager.CartItems.GetCartItems(userId, includeNavigationFields: true);

            var cartItemsToOrder = cart.Where(x => x.Size.Quantity > 0);

            var order = _repositoryManager.Orders.CreateOrder(cartItemsToOrder, userId);
            _repositoryManager.CartItems.DeleteCartItems(cart);

            await _repositoryManager.SaveAsync();

            await SendEmail(order, total);

            return RedirectToAction("Index", "Orders");
        }




        private string GetLoggedInUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private async Task SendEmail(Order order, string total)
        {
            var user = await _userManager.FindByIdAsync(GetLoggedInUserId());
            string orderStatusLink = $@"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host.Value}" +
                $@"/Orders/Status/{order.Id}";

            var subject = $"ShoeperStar Order Notification ({order.Id})";

            var mailBody = new StringBuilder();
            mailBody.Append($@"<p>Good day Mr./Ms. {user.LastName}");
            mailBody.Append("<p>Kindly pay your order thru any of the options below.</p>");
            mailBody.Append($@"<p><span style='font-weight: bold;'>Order Expiry:</span> {order.PaymentExpiry.ToString("dd MMM yyyy h:mm tt")}</p>");
            mailBody.Append($@"<p><span style='font-weight: bold;'>Amount: </span><span style='color:red;'>{total}</span></p>");
            mailBody.Append("<br>");
            mailBody.Append("<p style='font-weight: bold;'>Payment Options:</p>");
            mailBody.Append($@"<p style='font-weight: bold; color:blue;'>GCASH:</p>");
            mailBody.Append($@"<p>Account Name: <span style='color:red;'>ShoeperStar</span></p>");
            mailBody.Append($@"<p>Account No.: <span style='color:red;'>09123456789</span></p>");
            mailBody.Append("<br>");
            mailBody.Append($@"<p style='font-weight: bold; color:blue;'>BDO:</p>");
            mailBody.Append($@"<p>Account Name: <span style='color:red;'>ShoeperStar</span></p>");
            mailBody.Append($@"<p>Account No.: <span style='color:red;'>9087654321</span></p>");
            mailBody.Append("<br>");
            mailBody.Append($@"<p>Check Order Status: {orderStatusLink}</p>");
            mailBody.Append("<br>");
            mailBody.Append($@"<p style='font-weight: bold; color:blue;'>IMPORTANT:</p>");
            mailBody.Append($@"<p>   1)Your order will reservation will expire on <span style='color:red;'>{order.PaymentExpiry.ToString("dd MMM yyyy h:mm tt")}</span></p>");
            mailBody.Append($@"<p>   2)After payment has been made, kindly <span style='color:red;'>reply to this email with proof of payment </span>(picture or screenshot) as attachment</p>");


            var message = new Message(new string[] { user.Email }, subject, mailBody.ToString(), null);
            await _emailSender.SendEmailAsync(message);
        }
    }
}
