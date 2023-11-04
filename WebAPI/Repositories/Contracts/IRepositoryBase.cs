using System.Linq.Expressions;

namespace WebAPI.Repositories.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> FindAllAsync(bool trackchanges);
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackchanges);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
