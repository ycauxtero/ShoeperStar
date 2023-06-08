using ShoeperStar.Models;

namespace ShoeperStar.Data.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCatergories(bool trackChanges);
        Task<Category> GetCatergoryAsync(int catergoryId, bool trackChanges);
        void CreateCatergory(Category catergory);
        void DeleteCatergory(Category catergory);
        void UpdateCatergory(Category catergory);
    }
}
