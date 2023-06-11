using ShoeperStar.Data.Custom.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoeperStar.Models.ViewModels
{
    public class ShoeForCreationVM
    {
        public int Id { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Please input shoe model")]
        public string Name { get; set; }

        [MustBeGreaterThanZero]
        public string Price { get; set; }

        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "Please input image url")]
        public string ImageURL { get; set; }

        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a brand")]
        public int BrandId { get; set; }

        [Display(Name = "Gender")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a gender")]
        public int GenderId { get; set; }

        [Display(Name = "Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
    }
}
