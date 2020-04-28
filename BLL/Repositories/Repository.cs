using BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    /// <summary>
    /// Contains base repository methods for an entity type.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TContext">Database context.</typeparam>
    public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        protected TContext DbContext { get; }
        protected DbSet<TEntity> Entities { get; }

        public Repository(TContext context)
        {
            DbContext = context;
            Entities = context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
        }

        public virtual async Task<TEntity> GetAsync(params object[] keyValues)
        {
            return await Entities.FindAsync(keyValues);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Entities.ToArrayAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagedResultsAsync(int pageIndex, int pageSize)
        {
            var pageResults = pageIndex > 1 ?
                Entities.Skip((pageIndex - 1) * pageSize).Take(pageSize) :
                Entities.Take(pageSize);

            return await pageResults.ToArrayAsync();
        }
    }
}
