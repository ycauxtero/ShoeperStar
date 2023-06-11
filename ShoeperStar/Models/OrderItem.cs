using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeperStar.Models
{
    public class OrderItem
    {
        [Key]
        public long Id { get; set; }

        public int SizeId { get; set; }
        public Size Size { get; set; }

        public double TotalPrice { get; set; }
        public int Quantity { get; set; }

        public Guid OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
    }
}
