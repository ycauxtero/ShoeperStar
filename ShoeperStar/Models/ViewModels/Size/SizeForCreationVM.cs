using ShoeperStar.Data.Custom.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoeperStar.Models.ViewModels
{
    public class SizeForCreationVM
    {
        public int Id { get; set; }

        [Display(Name = "Size")]
        [Required(ErrorMessage = "Please input shoe size")]
        public string Value { get; set; }

        [MustBeGreaterThanZero]
        public string Quantity { get; set; }

        [Required(ErrorMessage = "Please input SKU")]
        public string SKU { get; set; }

        public int ShoeId { get; set; }
        public int VariantId { get; set; }

        public string ShoeModel { get; set; }
        public string Color { get; set; }
        public string ColorHex { get; set; }
    }
}
