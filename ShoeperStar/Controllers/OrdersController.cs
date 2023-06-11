using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Data.References;
using ShoeperStar.Models;
using ShoeperStar.Models.ViewModels;
using System.Drawing.Printing;
using System.Security.Claims;
using X.PagedList;

namespace ShoeperStar.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public OrdersController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? page = 1, int? pageSize = null)
        {
            if (page != null && page < 1) page = 1;

            var orders = await GetOrders();
            var ordersVM = _mapper.Map<IEnumerable<OrderVM>>(orders);

            return View(ordersVM.ToPagedList((int)page, GetPageSize(pageSize)));
        }

        public async Task<IActionResult> ReceiveOrder(Guid id)
        {
            var order = await _repositoryManager.Orders.GetOrderAsync(id, trackChanges: false);

            if (order == null) return RedirectToAction("Index");

            order.OrderRecieved = true;

            _repositoryManager.Orders.UpdateOrder(order);
            await _repositoryManager.SaveAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> ShipOutOrder(Guid id)
        {
            var order = await _repositoryManager.Orders.GetOrderAsync(id, trackChanges: false);

            if (order == null) return RedirectToAction(nameof(Index));

            order.IsShipped = true;

            _repositoryManager.Orders.UpdateOrder(order);
            await _repositoryManager.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> OrderPaid(Guid id)
        {
            var order = await _repositoryManager.Orders.GetOrderAsync(id, trackChanges: false);

            if (order == null) return RedirectToAction(nameof(Index));

            order.IsPaid = true;

            _repositoryManager.Orders.UpdateOrder(order);
            await _repositoryManager.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Expired()
        {
            var orders = await _repositoryManager.Orders.GetAllOrdersAsync(trackChanges: false);
            var expiredOrders = orders.Where(x => x.PaymentExpiry < DateTime.Now &&
                                                !x.IsCancelled &&
                                                !x.IsPaid)
                                      .ToList();

            return View(expiredOrders);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CancelExpiredOrders()
        {
            var orders = await _repositoryManager.Orders.GetAllOrdersAsync(trackChanges: false);
            var expiredOrders = orders.Where(x => x.PaymentExpiry < DateTime.Now &&
                                                    !x.IsCancelled &&
                                                    !x.IsPaid)
                                      .ToList();

            await CancelExpiredOrdersAndReturnStocksToInventory(expiredOrders);

            return RedirectToAction(nameof(Expired));
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Filter(string option, int? page = 1)
        {
            option ??= "unpaid-orders";

            var orders = await _repositoryManager.Orders.GetAllOrdersAsync(trackChanges: false);
            switch (option)
            {
                case "orders-for-shipping":
                    orders = orders.Where(x => x.IsPaid && !x.IsShipped);
                    break;
                case "shipped-orders":
                    orders = orders.Where(x => x.IsShipped && !x.OrderRecieved);
                    break;
                case "received-orders":
                    orders = orders.Where(x => x.OrderRecieved);
                    break;
                default:
                    orders = orders.Where(x => !x.IsCancelled && !x.IsPaid);
                    break;
            }

            ViewData["option"] = option;
            orders = orders.OrderByDescending(x => x.OrderDate);
            var ordersVM = _mapper.Map<IEnumerable<OrderVM>>(orders);

            return View("Index", ordersVM.ToPagedList((int)page, GetPageSize(null)));
        }










        private async Task<IEnumerable<Order>> GetOrders()
        {
            IEnumerable<Order> orders = null;

            if (User.IsInRole(UserRoles.Admin))
            {
                orders = await _repositoryManager.Orders.GetAllOrdersAsync(trackChanges: false);
                return await orders.Where(x => !x.IsCancelled)
                                    .OrderByDescending(x => !x.IsPaid)
                                    .ThenByDescending(x => !x.IsShipped)
                                    .ThenBy(x => x.OrderDate)
                                    .ToListAsync();
            }

            var userId = GetLoggedInUserId();
            orders = await _repositoryManager.Orders.GetAllOrdersAsync(userId, trackChanges: false);
            return await orders.OrderByDescending(x => x.OrderDate).ToListAsync();
        }

        private string GetLoggedInUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private int GetPageSize(int? pageSize)
        {
            if (pageSize != null)
            {
                HttpContext.Response.Cookies.Append("page-size", pageSize.ToString());
                return Convert.ToInt32(pageSize);
            }

            var pageSizeCookie = HttpContext.Request.Cookies["page-size"];
            if (pageSizeCookie == null)
            {
                pageSize = 1;
                HttpContext.Response.Cookies.Append("page-size", pageSize.ToString());
                return Convert.ToInt32(pageSize);
            }

            return Convert.ToInt32(pageSizeCookie);
        }

        private async Task CancelExpiredOrdersAndReturnStocksToInventory(IEnumerable<Order> expiredOrders)
        {
            var shoeSizesForStockUpdate = await GetShoeSizesForStocksUpdate(expiredOrders);
            var ordersForCancellation = GetOrdersForCancellationWithoutNavigationFields(expiredOrders);

            _repositoryManager.Sizes.UpdateSizes(shoeSizesForStockUpdate);
            _repositoryManager.Orders.UpdateOrders(ordersForCancellation);

            await _repositoryManager.SaveAsync();
        }

        private async Task<IEnumerable<Size>> GetShoeSizesForStocksUpdate(IEnumerable<Order> expiredOrders)
        {
            var shoeSizesToUpdate = expiredOrders
                                    .SelectMany(order =>
                                        order.OrderItems.Select(orderItem =>
                                            new
                                            {
                                                id = orderItem.SizeId,
                                                qty = orderItem.Quantity
                                            }
                                            ))
                                            .GroupBy(x => x.id)
                                            .Select(x => new
                                            {
                                                id = x.Key,
                                                qty = x.Select(x => x.qty).Sum()
                                            });

            var shoesSizes = new List<Size>();
            foreach (var shoeSize in shoeSizesToUpdate)
            {
                var movieFromDb = await _repositoryManager.Sizes.GetSizeAsync(shoeSize.id, trackChanges: false, includeNavigationFields: false);
                movieFromDb.Quantity += shoeSize.qty;

                shoesSizes.Add(movieFromDb);
            }

            return shoesSizes;
        }

        private IEnumerable<Order> GetOrdersForCancellationWithoutNavigationFields(IEnumerable<Order> expiredOrders)
        {
            var ordersForCancellation = new List<Order>();

            foreach (var order in expiredOrders)
            {
                ordersForCancellation.Add(new Order
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    PaymentExpiry = order.PaymentExpiry,
                    UserId = order.UserId,
                    IsCancelled = true,
                    IsPaid = order.IsPaid,
                    IsShipped = order.IsShipped,
                    OrderRecieved = order.OrderRecieved
                });
            }

            return ordersForCancellation;
        }

    }
}
