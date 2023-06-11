namespace ShoeperStar.Models.ViewModels
{
    public class CartItemVM
    {
        public uint Id { get; set; }

        public SizeVM Size { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public string Model { get; set; }
        public string ColorHex { get; set; }

        public string UserId { get; set; }
    }
}
