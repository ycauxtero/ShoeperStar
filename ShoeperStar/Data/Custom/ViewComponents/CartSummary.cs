using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.Contracts;

namespace ShoeperStar.Data.Custom.ViewComponents
{
    public class CartSummary : ViewComponent
    {
        private ICartRepository _cartRepository;

        public CartSummary(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public IViewComponentResult Invoke(string userId)
        {
            var userCartItems = _cartRepository.GetCartItems(userId).GetAwaiter().GetResult();
            var totalCartItemCount = userCartItems.Select(x => x.Qty).Sum();

            return View(totalCartItemCount);
        }
    }
}
