using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;

namespace ShoeperStar.Data.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
        public void CreateCatergory(Category catergory)
        {
            Create(catergory);
        }

        public void DeleteCatergory(Category catergory)
        {
            Delete(catergory);
        }

        public async Task<IEnumerable<Category>> GetAllCatergories(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<Category> GetCatergoryAsync(int catergoryId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == catergoryId, trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateCatergory(Category catergory)
        {
            Update(catergory);
        }
    }
}
