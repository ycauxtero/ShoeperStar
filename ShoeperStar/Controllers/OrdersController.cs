using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.Contracts;

namespace ShoeperStar.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public OrdersController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
