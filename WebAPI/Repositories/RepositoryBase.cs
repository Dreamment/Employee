using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAPI.Repositories.Contracts;

namespace WebAPI.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _repositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task CreateAsync(T entity) => await _repositoryContext.Set<T>().AddAsync(entity);

        public async Task DeleteAsync(T entity) => _repositoryContext.Set<T>().Remove(entity);

        public async Task<IEnumerable<T>> FindAllAsync(bool trackchanges) 
        {
            IEnumerable<T> entity;
            if (!trackchanges)
            {
                entity = (IEnumerable<T>)await _repositoryContext.Set<T>().AsNoTracking().ToListAsync();
            }
            else
            {
                entity = (IEnumerable<T>)await _repositoryContext.Set<T>().ToListAsync();
            }
            return entity;
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackchanges)
        {
            if (!trackchanges)
                return (IEnumerable<T>)await _repositoryContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
            return (IEnumerable<T>)await _repositoryContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task UpdateAsync(T entity) => _repositoryContext.Set<T>().Update(entity);
    }
}
