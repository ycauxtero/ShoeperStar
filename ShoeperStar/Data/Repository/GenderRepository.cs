using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;

namespace ShoeperStar.Data.Repository
{
    public class GenderRepository : RepositoryBase<Gender>, IGenderRepository
    {
        public GenderRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateGender(Gender gender)
        {
            Create(gender);
        }

        public void DeleteGender(Gender gender)
        {
            Delete(gender);
        }

        public async Task<IEnumerable<Gender>> GetAllGenders(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<Gender> GetGenderAsync(int genderId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == genderId, trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateGender(Gender gender)
        {
            Update(gender);
        }
    }
}
