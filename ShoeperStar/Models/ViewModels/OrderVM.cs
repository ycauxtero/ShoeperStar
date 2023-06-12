namespace ShoeperStar.Models.ViewModels
{
    public class OrderVM
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime PaymentExpiry { get; set; }
        public bool IsPaid { get; set; }
        public bool IsShipped { get; set; }
        public bool OrderRecieved { get; set; }
        public bool IsCancelled { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public IEnumerable<OrderItemVM> OrderItems { get; set; }
    }
}
