using System;
using System.Threading.Tasks;

namespace AuctionPortal.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Gets the entity with given id.
        /// </summary>
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Gets the entity with given id.
        /// </summary>
        Task<TEntity> GetAsync(Guid id, params string[] includes);

        /// <summary>
        /// Persists the given entity.
        /// </summary>
        void Create(TEntity entity);

        /// <summary>
        /// Updates the given entity.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an entity with the given id.
        /// </summary>
        void Delete(Guid id);
    }
}
