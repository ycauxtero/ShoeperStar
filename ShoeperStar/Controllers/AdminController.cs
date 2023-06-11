using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;
using ShoeperStar.Models.DTO;
using ShoeperStar.Models.ViewModels;

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

        [Route("Shoe/Create")]
        [HttpGet]
        public async Task<IActionResult> Create(int? sh_id)
        {
            var (brands, genders, catergories, shoes) = await GetDropdownSelectList();

            ViewBag.Brands = brands;
            ViewBag.Genders = genders;
            ViewBag.Categories = catergories;
            ViewBag.Shoes = shoes;

            if (sh_id == null)
                return View(new ShoeForCreationVM());


            var shoeForCreationVM = _mapper.Map<ShoeForCreationVM>(await _repositoryManager.Shoes.GetShoeAsync((int)sh_id, trackChanges: false));

            return View(shoeForCreationVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShoeForCreationVM shoeVM)
        {
            if (!ModelState.IsValid)
            {
                var (brands, genders, catergories, shoes) = await GetDropdownSelectList();

                ViewBag.Brands = brands;
                ViewBag.Genders = genders;
                ViewBag.Categories = catergories;
                ViewBag.Shoes = shoes;

                return View(shoeVM);
            }

            var shoe = _mapper.Map<Shoe>(shoeVM);

            _repositoryManager.Shoes.CreateShoe(shoe);
            await _repositoryManager.SaveAsync();

            return RedirectToAction(nameof(Variant), new { sh_id = shoe.Id });
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> UpdateShoe(ShoeForCreationVM shoeVM)
        {
            if (!ModelState.IsValid)
            {
                var (brands, genders, catergories, shoes) = await GetDropdownSelectList();

                ViewBag.Brands = brands;
                ViewBag.Genders = genders;
                ViewBag.Categories = catergories;
                ViewBag.Shoes = shoes;

                return View("Create", shoeVM);
            }

            var shoe = _mapper.Map<Shoe>(shoeVM);

            _repositoryManager.Shoes.UpdateShoe(shoe);
            await _repositoryManager.SaveAsync();

            return RedirectToAction(nameof(Create));
        }



        [Route("Shoe/Variant")]
        [HttpGet]
        public async Task<IActionResult> Variant(int sh_id, int? v_id)
        {
            var shoe = await _repositoryManager.Shoes.GetShoeAsync(sh_id, trackChanges: false);

            ViewBag.ShoeVariants = _mapper.Map<IEnumerable<VariantVM>>(
                                        await _repositoryManager.Variants.GetVariantsByShoeIdAsync(sh_id, trackChanges: false));

            if (v_id == null)
            {
                var variantForCreationVM = new VariantForCreationVM();
                variantForCreationVM.ShoeId = shoe.Id;
                variantForCreationVM.ShoeModel = shoe.Name;

                return View(variantForCreationVM);
            }

            var variantVM = _mapper.Map<VariantForCreationVM>(
                                    await _repositoryManager.Variants
                                            .GetVariantAsync((int)v_id, trackChanges: false, includeNavigationFields: true));

            return View(variantVM);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> CreateVariant(VariantForCreationVM variantVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShoeVariants = _mapper.Map<IEnumerable<VariantVM>>(
                                        await _repositoryManager.Variants.GetVariantsByShoeIdAsync(variantVM.ShoeId, trackChanges: false));
                ModelState.AddModelError("", "Please select a color variant");
                return View("Variant", variantVM);
            }

            var variant = _mapper.Map<Variant>(variantVM);
            _repositoryManager.Variants.CreateVariant(variant);

            await _repositoryManager.SaveAsync();

            return RedirectToAction("Variant", new { sh_id = variantVM.ShoeId });
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> UpdateVariant(VariantForCreationVM variantVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShoeVariants = await _repositoryManager.Variants.GetVariantsByShoeIdAsync(variantVM.ShoeId, trackChanges: false);
                ModelState.AddModelError("", "Please select a color variant");
                return View(variantVM);
            }

            var variant = _mapper.Map<Variant>(variantVM);
            _repositoryManager.Variants.UpdateVariant(variant);

            await _repositoryManager.SaveAsync();

            return RedirectToAction("Variant", new { sh_id = variantVM.ShoeId });
        }



        [Route("[action]/{v_Id}")]
        [HttpGet]
        public async Task<IActionResult> Size(int v_id, int? s_id)
        {
            var variant = await _repositoryManager.Variants.GetVariantAsync(v_id, trackChanges: false, includeNavigationFields: true);

            if (variant is null)
            {
                return View("NotFound");
            }

            ViewBag.VariantSizes = _mapper.Map<IEnumerable<SizeVM>>(
                                            await _repositoryManager.Sizes.GetSizesByVariantIdAsync(v_id, trackChanges: false));

            if (s_id == null)
            {
                var sizeVM = _mapper.Map<SizeForCreationVM>(variant);
                return View(sizeVM);
            }

            var shoeSize = await _repositoryManager.Sizes.GetSizeAsync((int)s_id, trackChanges: false, includeNavigationFields: true);
            var shoeSizeVM = _mapper.Map<SizeForCreationVM>(shoeSize);

            return View(shoeSizeVM);
        }
















        private async Task<(SelectList brands, SelectList genders, SelectList catergories, IEnumerable<ShoeVM> shoes)> GetDropdownSelectList()
        {
            return (
                new SelectList(await _repositoryManager.Brands.GetAllBrands(trackChanges: false),
                                nameof(Shoe.Id), nameof(Shoe.Name), 0),
                new SelectList(await _repositoryManager.Genders.GetAllGenders(trackChanges: false),
                                nameof(Gender.Id), nameof(Gender.Name), 0),
                new SelectList(await _repositoryManager.Categories.GetAllCatergories(trackChanges: false),
                                nameof(Category.Id), nameof(Category.Name), 0),
                _mapper.Map<IEnumerable<ShoeVM>>(await _repositoryManager.Shoes.GetAllShoes(trackChanges: false))
            );
        }

    }
}
