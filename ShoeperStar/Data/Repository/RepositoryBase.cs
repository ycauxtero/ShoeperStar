using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using System.Linq.Expressions;

namespace ShoeperStar.Data.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext RepositoryContext;
        public RepositoryBase(AppDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

        public async Task CreateMultiple(IEnumerable<T> entities)
        {
            await RepositoryContext.Set<T>().AddRangeAsync(entities);
        }

        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

        public void DeleteMultiple(IEnumerable<T> entities)
        {
            RepositoryContext.Set<T>().RemoveRange(entities);
        }

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ?
            RepositoryContext.Set<T>().AsNoTracking() : RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? RepositoryContext.Set<T>().Where(expression).AsNoTracking()
            : RepositoryContext.Set<T>().Where(expression);

        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

        public void UpdateMultiple(IEnumerable<T> entities)
        {
            RepositoryContext.Set<T>().UpdateRange(entities);
        }
    }
}
