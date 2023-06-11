using ShoeperStar.Models;

namespace ShoeperStar.Data.Contracts
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetCartItems(string userId, bool includeNavigationFields = false);
        Task<IEnumerable<CartItem>> GetCartItemsBasedOnActualStocks(string userId);
        Task AddCartItem(CartItem cartItem);
        Task AddCartItem(int shoeSizeId, string userId);
        Task DeleteCartItem(CartItem cartItem);
        Task DeleteCartItems(string userId);
        void DeleteCartItems(IEnumerable<CartItem> cartItems);
        void UpdateCartItem(CartItem cartItem);
    }
}
