using System.Linq.Expressions;

namespace WebAPI.Repositories
{
    public interface IRepositoryBase<T> where T: class
    {
        IQueryable<T> FindAll(bool trackchanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackchanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
