using ShoeperStar.Models;

namespace ShoeperStar.Data.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(bool trackChanges, bool includeNavigationFields = true);
        Task<IEnumerable<Order>> GetAllOrdersAsync(string userId, bool trackChanges, bool includeNavigationFields = true);
        Task<Order> GetOrderAsync(Guid id, bool trackChanges, bool includeNavigationFields = true);
        void CreateOrder(Order order);
        void CreateOrder(IEnumerable<CartItem> cartItems, string userId);
        void UpdateOrder(Order order);
        void UpdateOrders(IEnumerable<Order> orders);
        void DeleteOrder(Order order);
    }
}
