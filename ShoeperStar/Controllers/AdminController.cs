using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models.DTO;

namespace ShoeperStar.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AdminController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [Route("[action]")]
        public async Task<IActionResult> Brands(BaseForNavModelDTO dto)
        {
            var items = await _repositoryManager.Brands.GetAllBrands(trackChanges: false);

            ViewBag.Title = "All Brands";
            ViewBag.Items = items;
            dto.Category = "Brand";

            return View("ShoeFilters", dto);
        }
    }
}
