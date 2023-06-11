using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;
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

        [Route("[action]")]
        public async Task<IActionResult> Genders(BaseForNavModelDTO dto)
        {
            var items = await _repositoryManager.Genders.GetAllGenders(trackChanges: false);

            ViewBag.Title = "All Gender";
            ViewBag.Items = items;
            dto.Category = "Gender";

            return View("ShoeFilters", dto);
        }

        [Route("[action]")]
        public async Task<IActionResult> Categories(BaseForNavModelDTO dto)
        {
            var items = await _repositoryManager.Categories.GetAllCatergories(trackChanges: false);

            ViewBag.Title = "All Category";
            ViewBag.Items = items;
            dto.Category = "Category";

            return View("ShoeFilters", dto);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Add(BaseForNavModelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View("ShoeFilters", dto);
            }

            switch (dto.Category)
            {
                case "Brand":
                    _repositoryManager.Brands.CreateBrand(new Brand { Name = dto.Name });
                    await _repositoryManager.SaveAsync();
                    return RedirectToAction(nameof(Brands));

                case "Gender":
                    _repositoryManager.Genders.CreateGender(new Gender { Name = dto.Name });
                    await _repositoryManager.SaveAsync();
                    return RedirectToAction(nameof(Genders));

                case "Category":
                    _repositoryManager.Categories.CreateCatergory(new Category { Name = dto.Name });
                    await _repositoryManager.SaveAsync();
                    return RedirectToAction(nameof(Categories));
                default:
                    return View("Error");
            }
        }
    }
}
