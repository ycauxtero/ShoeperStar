using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;

namespace ShoeperStar.Data.Repository
{
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateBrand(Brand brand)
        {
            Create(brand);
        }

        public void DeleteBrand(Brand brand)
        {
            Delete(brand);
        }

        public async Task<IEnumerable<Brand>> GetAllBrands(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<Brand> GetBrandAsync(int brandId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == brandId, trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateBrand(Brand brand)
        {
            Update(brand);
        }
    }
}
