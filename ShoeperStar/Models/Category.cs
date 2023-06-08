namespace ShoeperStar.Models
{
    public class Category : BaseForNavigationModels
    {
        public ICollection<Shoe> Shoes { get; set; }
    }
}
