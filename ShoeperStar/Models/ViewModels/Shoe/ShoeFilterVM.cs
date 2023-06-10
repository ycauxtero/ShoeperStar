namespace ShoeperStar.Models.ViewModels
{
    public class ShoeFilterVM
    {
        public int BrandFilter { get; set; }
        public int CategoryFilter { get; set; }
        public int GenderFilter { get; set; }
        public string ModelFilter { get; set; }
        public string SortField { get; set; } = nameof(Shoe.Name);
        public string SortOrder { get; set; } = "asc";
    }
}
