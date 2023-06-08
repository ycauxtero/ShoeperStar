namespace ShoeperStar.Models
{
    public class Brand : BaseForNavigationModels
    {
        public ICollection<Shoe> Shoes { get; set; }
    }
}
