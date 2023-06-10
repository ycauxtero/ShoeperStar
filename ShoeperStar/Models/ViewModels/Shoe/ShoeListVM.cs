using X.PagedList;

namespace ShoeperStar.Models.ViewModels
{
    public class ShoeListVM
    {
        public IPagedList<ShoeVM> Shoes { get; set; }
        public IEnumerable<BrandVM> Brands { get; set; }
        public IEnumerable<CategoryVM> Categories { get; set; }
        public IEnumerable<GenderVM> Genders { get; set; }
        public Dictionary<string, string> SortFields =>
            new Dictionary<string, string> {
                { nameof(ShoeVM.Name), "Model" },
                { nameof(ShoeVM.BrandName), nameof(Brand) },
                { nameof(ShoeVM.CategoryName), nameof(Category) },
                { nameof(ShoeVM.GenderName), nameof(Gender) }
        };
        public Dictionary<string, string> SortOrders =>
            new Dictionary<string, string> {
                { "asc", "A to Z" },
                { "desc", "Z to A" }
        };

        public ShoeFilterVM ShoeFilter { get; set; }
    }
}
