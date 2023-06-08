using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Extensions;
using ShoeperStar.Models;

namespace ShoeperStar.Data.Repository
{
    public class SizeRepository : RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateSize(Size size)
        {
            Create(size);
        }

        public void DeleteSize(Size size)
        {
            Delete(size);
        }

        public async Task<IEnumerable<Size>> GetAllSizes(bool trackChanges, bool includeNavigationFields = false)
        {
            if (includeNavigationFields)
            {
                return await FindAll(trackChanges)
                            .IncludeSizeNavigationFields()
                            .ToListAsync();
            }

            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<Size> GetSizeAsync(int sizeId, bool trackChanges, bool includeNavigationFields = false)
        {
            if (includeNavigationFields)
            {
                return await FindByCondition(x => x.Id.Equals(sizeId), trackChanges)
                            .IncludeSizeNavigationFields()
                            .SingleOrDefaultAsync();

            }

            return await FindByCondition(x => x.Id.Equals(sizeId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Size>> GetSizesByVariantIdAsync(int variantId, bool trackChanges, bool includeNavigationFields = false)
        {
            if (includeNavigationFields)
            {
                return await FindByCondition(x => x.VariantId.Equals(variantId), trackChanges)
                    .IncludeSizeNavigationFields()
                    .ToListAsync();

            }
            return await FindByCondition(x => x.VariantId.Equals(variantId), trackChanges).ToListAsync();
        }

        public void UpdateSize(Size size)
        {
            Update(size);
        }

        public void UpdateSizes(IEnumerable<Size> sizes)
        {
            UpdateMultiple(sizes);
        }
    }
}
