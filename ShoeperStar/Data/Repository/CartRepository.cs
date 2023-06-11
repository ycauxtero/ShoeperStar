using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Extensions;
using ShoeperStar.Models;

namespace ShoeperStar.Data.Repository
{
    public class CartRepository : RepositoryBase<CartItem>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CartItem>> GetCartItems(string userId, bool includeNavigationFields = false)
        {
            if (includeNavigationFields)
            {
                return await FindByCondition(x => x.UserId == userId, trackChanges: false)
                            .IncludeCartItemNavigationFields()
                            .ToListAsync();
            }

            return await FindByCondition(x => x.UserId == userId, trackChanges: false).ToListAsync();
        }

        public async Task AddCartItem(CartItem cartItem)
        {
            var cartItemFromDb = await FindByCondition(x => x.UserId == cartItem.UserId &&
                                                            x.Size.Id == cartItem.Size.Id,
                                                        trackChanges: true)
                                                .FirstOrDefaultAsync();

            if (cartItemFromDb == null)
            {
                Create(cartItem);
                return;
            }

            cartItemFromDb.Qty++;
        }

        public async Task AddCartItem(int shoeSizeId, string userId)
        {
            var shoeSize = await RepositoryContext.Sizes.Where(s => s.Id.Equals(shoeSizeId)).SingleOrDefaultAsync();

            await AddCartItem(
                    new CartItem
                    {
                        UserId = userId,
                        Qty = 1,
                        Size = shoeSize
                    });
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            Update(cartItem);
        }

        public async Task DeleteCartItem(CartItem cartItem)
        {
            var shoeSizeHasZeroStocks = cartItem.Size.Quantity == 0;
            if (shoeSizeHasZeroStocks)
            {
                Delete(cartItem);
                return;
            }

            var cartItemFromDb = await FindByCondition(x => x.UserId == cartItem.UserId &&
                                                            x.Size.Id == cartItem.Size.Id,
                                                        trackChanges: true)
                                                .FirstOrDefaultAsync();

            if (cartItemFromDb.Qty > 1)
            {
                cartItemFromDb.Qty--;
                return;
            }

            Delete(cartItemFromDb);
        }

        public async Task DeleteCartItems(string userId)
        {
            var cartItemsOfUser = await FindByCondition(x => x.UserId == userId, trackChanges: false).ToListAsync();

            if (cartItemsOfUser == null)
                return;

            DeleteMultiple(cartItemsOfUser);
        }

        public void DeleteCartItems(IEnumerable<CartItem> cartItems)
        {
            DeleteMultiple(cartItems);
        }


        public async Task<IEnumerable<CartItem>> GetCartItemsBasedOnActualStocks(string userId)
        {
            var cart = await GetCartItems(userId, includeNavigationFields: true);

            var cartItemsWithQtyGreaterThanAvailableStock = cart.Where(x => x.Qty > x.Size.Quantity &&
                                                                            x.Size.Quantity > 0);
            if (cartItemsWithQtyGreaterThanAvailableStock.Count() == 0)
                return cart;

            foreach (var item in cartItemsWithQtyGreaterThanAvailableStock)
            {
                item.Qty = item.Size.Quantity;
                UpdateCartItem(item);
            }

            await RepositoryContext.SaveChangesAsync();

            return await GetCartItems(userId, includeNavigationFields: true);
        }


    }
}
