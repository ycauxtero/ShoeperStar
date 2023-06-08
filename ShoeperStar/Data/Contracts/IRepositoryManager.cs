namespace ShoeperStar.Data.Contracts
{
    public interface IRepositoryManager
    {
        IBrandRepository Brands { get; }
        IGenderRepository Genders { get; }
        ICategoryRepository Categories { get; }
        IShoeRepository Shoes { get; }
        IVariantRepository Variants { get; }
        ISizeRepository Sizes { get; }
        Task SaveAsync();
    }
}
