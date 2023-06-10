using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Data.Repository.Query;
using ShoeperStar.Extensions;
using ShoeperStar.Models;
using ShoeperStar.Models.DTO;
using ShoeperStar.Models.ViewModels;

namespace ShoeperStar.Data.Repository
{
    public class ShoeRepository : RepositoryBase<Shoe>, IShoeRepository
    {
        public ShoeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Shoe> GetShoeAsync(int shoeId, bool trackChanges, bool includeNavigationFields = false)
        {
            if (includeNavigationFields)
            {
                return await FindByCondition(x => x.Id.Equals(shoeId), trackChanges)
                            .IncludeShoeNavigationFields()
                            .SingleOrDefaultAsync();
            }

            return await FindByCondition(x => x.Id.Equals(shoeId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Shoe>> GetFilteredShoes(bool trackChanges, ShoeFilterVM filter, bool includeNavigationFields = false)
        {
            var filteredEmployeesQuery = FindByCondition(s =>
                                                            s.Variants.Any(v =>
                                                                v.Sizes.Any(x => x.Quantity > 0))
                                                        , trackChanges)
                                            .SearchShoes(filter.ModelFilter)
                                            .FilterShoes(new ShoeFilterDTO
                                            {
                                                BrandId = filter.BrandFilter,
                                                CategoryId = filter.CategoryFilter,
                                                GenderId = filter.GenderFilter
                                            })
                                            .SortShoes($"{filter.SortField} {filter.SortOrder}");
            if (includeNavigationFields)
            {
                return await filteredEmployeesQuery
                            .IncludeShoeNavigationFields()
                            .ToListAsync();
            }

            return await filteredEmployeesQuery.ToListAsync();
        }

        public async Task<IEnumerable<Shoe>> GetAllShoes(bool trackChanges, bool includeNavigationFields = false)
        {
            if (includeNavigationFields)
            {
                return await FindAll(trackChanges)
                            .IncludeShoeNavigationFields()
                            .ToListAsync();
            }
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Shoe>> GetByIds(IEnumerable<int> ids, bool trackChanges, bool includeNavigationFields = false)
        {
            if (includeNavigationFields)
            {
                return await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                            .IncludeShoeNavigationFields()
                            .ToListAsync();
            }

            return await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public void CreateShoe(Shoe shoe)
        {
            Create(shoe);
        }

        public void DeleteShoe(Shoe shoe)
        {
            Delete(shoe);
        }

        public void UpdateShoe(Shoe shoe)
        {
            Update(shoe);
        }


    }
}
