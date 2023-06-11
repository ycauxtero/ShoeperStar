using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;

namespace ShoeperStar.Data.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateOrder(Order order)
        {
            Create(order);
        }

        public void CreateOrder(IEnumerable<CartItem> cartItems, string userId)
        {
            var orderItems = new List<OrderItem>();
            foreach (var cartItem in cartItems)
            {
                orderItems.Add(new OrderItem
                {
                    SizeId = cartItem.Size.Id,
                    Quantity = cartItem.Qty,
                    TotalPrice = cartItem.Qty * cartItem.Size.Variant.Shoe.Price
                });
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderItems = orderItems,
            };

            Create(order);
        }

        public void DeleteOrder(Order order)
        {
            Delete(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(bool trackChanges, bool includeNavigationFields = true)
        {
            if (includeNavigationFields)
            {
                return await FindAll(trackChanges: false)
                            .Include(o => o.OrderItems)
                            .ThenInclude(oi => oi.Size)
                            .ThenInclude(s => s.Variant)
                            .ThenInclude(v => v.Shoe)
                            .ThenInclude(s => s.Brand)
                            .ToListAsync();
            }

            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(string userId, bool trackChanges, bool includeNavigationFields = true)
        {
            if (includeNavigationFields)
            {
                return await FindByCondition(x => x.UserId == userId, trackChanges: false)
                                            .Include(o => o.OrderItems)
                                            .ThenInclude(oi => oi.Size)
                                            .ThenInclude(s => s.Variant)
                                            .ThenInclude(v => v.Shoe)
                                            .ThenInclude(s => s.Brand)
                                            .ToListAsync();
            }

            return await FindByCondition(x => x.UserId == userId, trackChanges: false).ToListAsync();
        }

        public async Task<Order> GetOrderAsync(Guid id, bool trackChanges, bool includeNavigationFields = true)
        {
            if (includeNavigationFields)
            {
                return await FindByCondition(x => x.Id == id, trackChanges: false)
                                            .Include(x => x.OrderItems)
                                            .ThenInclude(x => x.Size)
                                            .SingleOrDefaultAsync();
            }

            return await FindByCondition(x => x.Id == id, trackChanges: false).SingleOrDefaultAsync();
        }

        public void UpdateOrder(Order order)
        {
            Update(order);
        }

        public void UpdateOrders(IEnumerable<Order> orders)
        {
            UpdateMultiple(orders);
        }
    }
}
