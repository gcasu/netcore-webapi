using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetAsync(params object[] keyValues);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetPagedResultsAsync(int pageIndex, int pageSize);
    }
}
