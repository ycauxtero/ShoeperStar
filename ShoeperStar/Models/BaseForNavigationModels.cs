using System.ComponentModel.DataAnnotations;

namespace ShoeperStar.Models
{
    public class BaseForNavigationModels
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
