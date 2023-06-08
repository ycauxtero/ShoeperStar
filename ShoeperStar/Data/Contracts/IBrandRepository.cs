using ShoeperStar.Models;

namespace ShoeperStar.Data.Contracts
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllBrands(bool trackChanges);
        Task<Brand> GetBrandAsync(int brandId, bool trackChanges);
        void CreateBrand(Brand brand);
        void DeleteBrand(Brand brand);
        void UpdateBrand(Brand brand);
    }
}
