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

        [Route("[action]")]
        public async Task<IActionResult> Edit(int id, string q)
        {
            switch (q)
            {
                case "Brand":
                    var brand = await _repositoryManager.Brands.GetBrandAsync(id, trackChanges: false);
                    return RedirectToAction(nameof(Brands), new { Id = brand.Id, Name = brand.Name, Categories = "Brand" });
                case "Gender":
                    var gender = await _repositoryManager.Genders.GetGenderAsync(id, trackChanges: false);
                    return RedirectToAction(nameof(Genders), new { Id = gender.Id, Name = gender.Name, Categories = "Gender" });
                case "Category":
                    var category = await _repositoryManager.Categories.GetCatergoryAsync(id, trackChanges: false);
                    return RedirectToAction(nameof(Categories), new { Id = category.Id, Name = category.Name, Categories = "Category" });
                default:
                    return View("Error");
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Update(BaseForNavModelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View("ShoeFilters", dto);
            }

            switch (dto.Category)
            {
                case "Brand":
                    _repositoryManager.Brands.UpdateBrand(new Brand { Id = dto.Id, Name = dto.Name });
                    await _repositoryManager.SaveAsync();
                    return RedirectToAction(nameof(Brands));

                case "Gender":
                    _repositoryManager.Genders.UpdateGender(new Gender { Id = dto.Id, Name = dto.Name });
                    await _repositoryManager.SaveAsync();
                    return RedirectToAction(nameof(Genders));

                case "Category":
                    _repositoryManager.Categories.UpdateCatergory(new Category { Id = dto.Id, Name = dto.Name });
                    await _repositoryManager.SaveAsync();
                    return RedirectToAction(nameof(Categories));
                default:
                    return View("Error");
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Delete(BaseForNavModelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View("ShoeFilters", dto);
            }

            switch (dto.Category)
            {
                case "Brand":
                    var brand = await _repositoryManager.Brands.GetBrandAsync(dto.Id, trackChanges: false);
                    _repositoryManager.Brands.DeleteBrand(brand);
                    await _repositoryManager.SaveAsync();
                    return RedirectToAction(nameof(Brands));

                case "Gender":
                    var gender = await _repositoryManager.Genders.GetGenderAsync(dto.Id, trackChanges: false);
                    _repositoryManager.Genders.DeleteGender(gender);
                    await _repositoryManager.SaveAsync();
                    return RedirectToAction(nameof(Genders));

                case "Category":
                    var category = await _repositoryManager.Categories.GetCatergoryAsync(dto.Id, trackChanges: false);
                    _repositoryManager.Categories.DeleteCatergory(category);
                    await _repositoryManager.SaveAsync();
                    return RedirectToAction(nameof(Categories));
                default:
                    return View("Error");
            }
        }




    }
}
