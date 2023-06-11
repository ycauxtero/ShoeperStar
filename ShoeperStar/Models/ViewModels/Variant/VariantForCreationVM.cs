using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoeperStar.Models.ViewModels
{
    public class VariantForCreationVM
    {
        public int Id { get; set; }

        [Display(Name = "Color")]
        [Required]
        public string Color { get; set; }

        [Display(Name = "Hex Value")]
        [Required]
        public string ColorHex { get; set; }

        public int ShoeId { get; set; }
        public string? ShoeModel { get; set; }

    }
}
