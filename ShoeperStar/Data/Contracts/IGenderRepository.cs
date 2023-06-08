using ShoeperStar.Models;

namespace ShoeperStar.Data.Contracts
{
    public interface IGenderRepository
    {
        Task<IEnumerable<Gender>> GetAllGenders(bool trackChanges);
        Task<Gender> GetGenderAsync(int genderId, bool trackChanges);
        void CreateGender(Gender gender);
        void DeleteGender(Gender gender);
        void UpdateGender(Gender gender);
    }
}
