using System.ComponentModel.DataAnnotations;

namespace ShoeperStar.Models.DTO
{
    public class BaseForNavModelDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
