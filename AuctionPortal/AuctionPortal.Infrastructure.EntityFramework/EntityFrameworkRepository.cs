using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using AuctionPortal.Infrastructure.EntityFramework.UnitOfWork;
using AuctionPortal.Infrastructure.UnitOfWork;

namespace AuctionPortal.Infrastructure.EntityFramework
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IUnitOfWorkProvider provider;

        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        protected DbContext Context => ((EntityFrameworkUnitOfWork)provider.GetUnitOfWorkInstance()).Context;

        public EntityFrameworkRepository(IUnitOfWorkProvider provider)
        {
            this.provider = provider;
        }

        public void Create(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            Context.Set<TEntity>().Add(entity);
        }

        public void Delete(Guid id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Guid id, params string[] includes)
        {
            DbQuery<TEntity> ctx = Context.Set<TEntity>();
            foreach (var include in includes)
            {
                ctx = ctx.Include(include);
            }
            return await ctx
                .SingleOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public void Update(TEntity entity)
        {
            var foundEntity = Context.Set<TEntity>().Find(entity.Id);
            Context.Entry(foundEntity).CurrentValues.SetValues(entity);
        }
    }
}
