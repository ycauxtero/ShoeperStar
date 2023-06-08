using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ShoeperStar.Models
{
    public class Variant
    {
        [Key]
        public int Id { get; set; }
        public string Color { get; set; }
        public string ColorHex { get; set; }

        public int ShoeId { get; set; }
        [ForeignKey(nameof(ShoeId))]
        public Shoe Shoe { get; set; }

        public ICollection<Size> Sizes { get; set; }
    }
}
