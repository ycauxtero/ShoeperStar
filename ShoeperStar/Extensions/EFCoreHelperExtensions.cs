using Microsoft.EntityFrameworkCore;
using ShoeperStar.Models;

namespace ShoeperStar.Extensions
{
    public static class EFCoreHelperExtensions
    {
        public static IQueryable<Shoe> IncludeShoeNavigationFields(this IQueryable<Shoe> shoes)
        {
            return shoes
                    .Include(x => x.Brand)
                    .Include(x => x.Catergory)
                    .Include(x => x.Gender)
                    .Include(x => x.Variants)
                    .ThenInclude(x => x.Sizes);
        }

        public static IQueryable<Variant> IncludeVariantNavigationFields(this IQueryable<Variant> variants)
        {
            return variants
                    .Include(x => x.Shoe)
                    .Include(x => x.Sizes);
        }

        public static IQueryable<Size> IncludeSizeNavigationFields(this IQueryable<Size> sizes)
        {
            return sizes
                    .Include(x => x.Variant)
                    .ThenInclude(x => x.Shoe);
        }

        public static IQueryable<CartItem> IncludeCartItemNavigationFields(this IQueryable<CartItem> cartItems)
        {
            return cartItems
                    .Include(x => x.Size)
                    .ThenInclude(x => x.Variant)
                    .ThenInclude(x => x.Shoe);
        }

        public static IQueryable<Order> IncludeOrderNavigationFields(this IQueryable<Order> orders)
        {
            return orders
                    .Include(o => o.User)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Size)
                    .ThenInclude(s => s.Variant)
                    .ThenInclude(v => v.Shoe)
                    .ThenInclude(s => s.Brand);
        }
    }
}
