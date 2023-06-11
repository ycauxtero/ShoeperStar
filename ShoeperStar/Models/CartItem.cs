using System.ComponentModel.DataAnnotations;

namespace ShoeperStar.Models
{
    public class CartItem
    {
        [Key]
        public long Id { get; set; }

        public Size Size { get; set; }
        public int Qty { get; set; }

        public string UserId { get; set; }
    }
}
