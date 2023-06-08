using ShoeperStar.Models;

namespace ShoeperStar.Data.Contracts
{
    public interface IVariantRepository
    {
        Task<IEnumerable<Variant>> GetAllVariants(bool trackChanges, bool includeNavigationFields = false);
        Task<Variant> GetVariantAsync(int variantId, bool trackChanges, bool includeNavigationFields = false);
        Task<IEnumerable<Variant>> GetVariantsByShoeIdAsync(int shoeId, bool trackChanges, bool includeNavigationFields = false);
        void CreateVariant(Variant variant);
        void DeleteVariant(Variant variant);
        void UpdateVariant(Variant variant);
    }
}
