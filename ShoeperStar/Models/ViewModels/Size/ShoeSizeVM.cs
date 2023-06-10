namespace ShoeperStar.Models.ViewModels
{
    public class ShoeSizeVM
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int Quantity { get; set; }
        public string SKU { get; set; }

        public int VariantId { get; set; }
        public ShoeVariantVM Variant { get; set; }
    }
}
