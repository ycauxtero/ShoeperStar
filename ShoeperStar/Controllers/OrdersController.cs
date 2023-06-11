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
    }
}
