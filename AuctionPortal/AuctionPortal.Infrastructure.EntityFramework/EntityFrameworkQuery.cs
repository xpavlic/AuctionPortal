using AuctionPortal.Infrastructure.EntityFramework.UnitOfWork;
using AuctionPortal.Infrastructure.Query;
using AuctionPortal.Infrastructure.Query.Helpers;
using AuctionPortal.Infrastructure.Query.Predicates;
using AuctionPortal.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
            QueryResult<TEntity> result;
            var sql = new StringBuilder().Append($"{SqlConstants.SelectFromClause}[{new TEntity().TableName}] WITH (NOLOCK) ");

            if (Predicate != null)
            {
                var predicateResult = Predicate is CompositePredicate composite ?
                                            composite.BuildCompositePredicate() :
                                            (Predicate as SimplePredicate).BuildSimplePredicate();

                sql.Append($"{SqlConstants.WhereClause}{predicateResult}");
            }

            if (!string.IsNullOrWhiteSpace(SortAccordingTo))
            {
                sql.Append(SqlConstants.OrderByClause + SortAccordingTo + (UseAscendingOrder ? SqlConstants.Ascending : SqlConstants.Descending));
            }

            if (DesiredPage > 0)
            {
                var items = (await Context.Set<TEntity>().SqlQuery(sql.ToString()).ToListAsync()).Skip((DesiredPage.Value - 1) * PageSize).Take(PageSize).ToList();
                result = new QueryResult<TEntity>(items, items.Count, PageSize, DesiredPage);
            }
            else
            {
                List<TEntity> items = await Context.Database.SqlQuery<TEntity>(sql.ToString()).ToListAsync();
                result = new QueryResult<TEntity>(items, items.Count);
            }
            return result;
        }
    }
}
