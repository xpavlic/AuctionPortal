using AuctionPortal.Infrastructure.EntityFramework.UnitOfWork;
using AuctionPortal.Infrastructure.Query;
using AuctionPortal.Infrastructure.UnitOfWork;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AuctionPortal.Infrastructure.EntityFramework
{
    public class EntityFrameworkQuery<TEntity> : QueryBase<TEntity> where TEntity : class, IEntity, new()
    {
        protected DbContext Context => ((EntityFrameworkUnitOfWork)Provider.GetUnitOfWorkInstance()).Context;

        /// <summary>
        ///   Initializes a new instance of the <see cref="EntityFrameworkQuery{TResult}" /> class.
        /// </summary>
        public EntityFrameworkQuery(IUnitOfWorkProvider provider) : base(provider) { }

        public override async Task<QueryResult<TEntity>> ExecuteAsync()
        {
            // TODO...

            throw new NotImplementedException();
        }
    }
}
