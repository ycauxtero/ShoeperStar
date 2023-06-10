namespace ShoeperStar.Models.ViewModels
{
    public class ShoeVariantVM
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string ColorHex { get; set; }

        public int ShoeId { get; set; }
        public ShoeVM Shoe { get; set; }

        public IEnumerable<SizeVM> Sizes { get; set; }
    }
}
