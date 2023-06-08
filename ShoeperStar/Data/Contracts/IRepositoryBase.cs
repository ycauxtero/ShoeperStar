using System.Linq.Expressions;

namespace ShoeperStar.Data.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        Task CreateMultiple(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateMultiple(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteMultiple(IEnumerable<T> entities);
    }
    
}
