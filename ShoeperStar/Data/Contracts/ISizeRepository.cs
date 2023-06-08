using ShoeperStar.Models;

namespace ShoeperStar.Data.Contracts
{
    public interface ISizeRepository
    {
        Task<IEnumerable<Size>> GetAllSizes(bool trackChanges, bool includeNavigationFields = false);
        Task<Size> GetSizeAsync(int sizeId, bool trackChanges, bool includeNavigationFields = false);
        Task<IEnumerable<Size>> GetSizesByVariantIdAsync(int variantId, bool trackChanges, bool includeNavigationFields = false);
        void CreateSize(Size size);
        void DeleteSize(Size size);
        void UpdateSize(Size size);
        void UpdateSizes(IEnumerable<Size> sizes);
    }
}
