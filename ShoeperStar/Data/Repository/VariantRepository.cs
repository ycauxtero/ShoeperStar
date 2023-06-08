using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Extensions;
using ShoeperStar.Models;

namespace ShoeperStar.Data.Repository
{
    public class VariantRepository : RepositoryBase<Variant>, IVariantRepository
    {
        public VariantRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateVariant(Variant variant)
        {
            Create(variant);
        }

        public void DeleteVariant(Variant variant)
        {
            Delete(variant);
        }

        public async Task<IEnumerable<Variant>> GetAllVariants(bool trackChanges, bool includeNavigationFields = false)
        {
            if (includeNavigationFields)
            {
                return await FindAll(trackChanges)
                                .IncludeVariantNavigationFields()
                                .ToListAsync();
            }

            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Variant>> GetVariantsByShoeIdAsync(int shoeId, bool trackChanges, bool includeNavigationFields = false)
        {
            if (includeNavigationFields)
            {
                return await FindByCondition(x => x.ShoeId.Equals(shoeId), trackChanges)
                                .IncludeVariantNavigationFields()
                                .ToListAsync();
            }

            return await FindByCondition(x => x.ShoeId.Equals(shoeId), trackChanges).ToListAsync();
        }

        public async Task<Variant> GetVariantAsync(int variantId, bool trackChanges, bool includeNavigationFields = false)
        {
            if (includeNavigationFields)
            {
                return await FindByCondition(x => x.Id.Equals(variantId), trackChanges)
                                .IncludeVariantNavigationFields()
                                .SingleOrDefaultAsync();

            }
            return await FindByCondition(x => x.Id.Equals(variantId), trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateVariant(Variant variant)
        {
            Update(variant);
        }
    }
}
