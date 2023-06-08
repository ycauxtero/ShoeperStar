using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;
using ShoeperStar.Models.ViewModels.Shoe;
using System.Diagnostics;

namespace ShoeperStar.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly Mapper _mapper;

        public HomeController(IRepositoryManager repositoryManager, Mapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // CHANGE IMPLEMENTATION DEPENDING ON WHAT TO SHOES TO DISPLAY
            var shoesToDisplay = await _repositoryManager.Shoes.GetAllShoes(trackChanges: false, includeNavigationFields: true);
            var shoesToisplayVM = _mapper.Map<IEnumerable<ShoeVM>>(shoesToDisplay);

            return View(shoesToisplayVM);
        }

    }
}