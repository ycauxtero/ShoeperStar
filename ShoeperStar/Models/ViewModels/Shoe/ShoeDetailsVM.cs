using ShoeperStar.Models.ViewModels.Size;
using System;

namespace ShoeperStar.Models.ViewModels.Shoe
{
    public class ShoeDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }

        public ShoeVariantVM SelectedVariant { get; set; }
        public ShoeSizeVM SelectedSize { get; set; }

        public BrandVM Brand { get; set; }
        public GenderVM Gender { get; set; }
        public CategoryVM Catergory { get; set; }

        public IEnumerable<ShoeVariantVM> Variants { get; set; }
        public IEnumerable<ShoeSizeVM> Sizes { get; set; }
    }
}
