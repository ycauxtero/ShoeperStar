namespace ShoeperStar.Models.ViewModels
{
    public class OrderItemVM
    {
        public long Id { get; set; }

        public int SizeId { get; set; }
        public ShoeSizeVM Size { get; set; }

        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
