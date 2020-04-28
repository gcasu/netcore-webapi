using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    /// <summary>
    /// Contains base repository methods for an entity type.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds a single entity of TEntity to the database.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <returns></returns>
        Task AddAsync(TEntity entity);
        /// <summary>
        /// Returns a single entity of TEntity from the database.
        /// </summary>
        /// <param name="keyValues">Key values.</param>
        /// <returns>A single entity of type TEntity.</returns>
        Task<TEntity> GetAsync(params object[] keyValues);
        /// <summary>
        /// Returns all the entities of TEntity from the database.
        /// </summary>
        /// <returns>A list of entities of type TEntity.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();
        /// <summary>
        /// Returns a paged result of entities of TEntity from the database.
        /// </summary>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>A list of entities of type TEntity.</returns>
        Task<IEnumerable<TEntity>> GetPagedResultsAsync(int pageIndex, int pageSize);
    }
}
