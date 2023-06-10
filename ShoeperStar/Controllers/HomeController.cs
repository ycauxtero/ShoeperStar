using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Data.Services;
using ShoeperStar.Models;
using ShoeperStar.Models.ViewModels;
using System.Diagnostics;
using X.PagedList;

namespace ShoeperStar.Controllers
{
    [Route("")]
    [Route("Shoes")]
    public class HomeController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly HashIdService _hashIdService;

        public HomeController(IRepositoryManager repositoryManager, IMapper mapper, HashIdService hashIdService)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _hashIdService = hashIdService;
        }

        [Route("")]
        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            // CHANGE IMPLEMENTATION DEPENDING ON WHAT TO SHOES TO DISPLAY
            var shoesToDisplay = await _repositoryManager.Shoes.GetAllShoes(trackChanges: false, includeNavigationFields: true);
            var shoesToisplayVM = _mapper.Map<IEnumerable<ShoeVM>>(shoesToDisplay);

            return View(shoesToisplayVM);
        }

        [Route("[action]/{shoeVariantId}/{shoeSizeId}/{slug}")]
        public async Task<IActionResult> Details(string shoeSizeId, string shoeVariantId)
        {
            var intShoeSizeId = _hashIdService.Decode(shoeSizeId);
            var intShoeVariantId = _hashIdService.Decode(shoeVariantId);

            var shoeSize = await _repositoryManager.Sizes.GetSizeAsync(intShoeSizeId, trackChanges: false, includeNavigationFields: true);
            var shoeSizeVM = _mapper.Map<ShoeSizeVM>(shoeSize);

            var shoeVariant = await _repositoryManager.Variants.GetVariantAsync(intShoeVariantId, trackChanges: false);
            var shoeVariantVM = _mapper.Map<ShoeVariantVM>(shoeVariant);

            var variantSizes = await _repositoryManager.Sizes.GetSizesByVariantIdAsync(intShoeVariantId, trackChanges: false);
            var sizesVM = _mapper.Map<IEnumerable<ShoeSizeVM>>(variantSizes);

            var shoeFromDb = await _repositoryManager.Shoes.GetShoeAsync(shoeSizeVM.Variant.ShoeId, trackChanges: false, includeNavigationFields: true);

            var shoeDetailsVM = _mapper.Map<ShoeDetailsVM>(shoeFromDb);

            shoeDetailsVM.SelectedVariant = shoeVariantVM;
            shoeDetailsVM.Sizes = sizesVM;
            shoeDetailsVM.SelectedSize = shoeSizeVM;

            return View(shoeDetailsVM);
        }

        [Route("View/All")]
        public async Task<IActionResult> Filter(ShoeFilterVM filter, int page = 1)
        {
            var shoeListVM = new ShoeListVM();
            var shoes = await _repositoryManager.Shoes.GetFilteredShoes(trackChanges: false, filter, includeNavigationFields: true);

            var shoesVM = _mapper.Map<IEnumerable<ShoeVM>>(shoes);
            shoeListVM.Shoes = shoesVM.ToPagedList(page, 8);
            shoeListVM.Brands =
                _mapper.Map<IEnumerable<BrandVM>>(await _repositoryManager.Brands.GetAllBrands(trackChanges: false));
            shoeListVM.Categories =
                _mapper.Map<IEnumerable<CategoryVM>>(await _repositoryManager.Categories.GetAllCatergories(trackChanges: false));
            shoeListVM.Genders =
                _mapper.Map<IEnumerable<GenderVM>>(await _repositoryManager.Genders.GetAllGenders(trackChanges: false));

            shoeListVM.ShoeFilter = filter;

            return View(shoeListVM);
        }
    }
}