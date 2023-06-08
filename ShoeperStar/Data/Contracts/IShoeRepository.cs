using ShoeperStar.Models;

namespace ShoeperStar.Data.Contracts
{
    public interface IShoeRepository
    {
        Task<IEnumerable<Shoe>> GetAllShoes(bool trackChanges, bool includeNavigationFields = false);
        Task<Shoe> GetShoeAsync(int shoeId, bool trackChanges, bool includeNavigationFields = false);
        void CreateShoe(Shoe shoe);
        Task<IEnumerable<Shoe>> GetByIds(IEnumerable<int> ids, bool trackChanges, bool includeNavigationFields = false);
        void DeleteShoe(Shoe shoe);
        void UpdateShoe(Shoe shoe);
    }
}
