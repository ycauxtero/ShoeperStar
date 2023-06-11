using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeperStar.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime PaymentExpiry { get; set; }
        public bool IsPaid { get; set; }
        public bool IsShipped { get; set; }
        public bool OrderRecieved { get; set; }
        public bool IsCancelled { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
