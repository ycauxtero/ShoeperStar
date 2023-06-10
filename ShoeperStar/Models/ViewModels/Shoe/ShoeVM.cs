using ShoeperStar.Models.ViewModels;

namespace ShoeperStar.Models.ViewModels
{
    public class ShoeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<ShoeVariantVM> Variants { get; set; }
    }
}
