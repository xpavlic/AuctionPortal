using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.QueryObjects.Common;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure.Query;
using AuctionPortal.Infrastructure.Query.Predicates;
using AuctionPortal.Infrastructure.Query.Predicates.Operators;
using AutoMapper;

namespace AuctionPortal.BusinessLayer.QueryObjects
{
	public class AccountQueryObject : QueryObjectBase<AccountDTO, Account, AccountFilterDto, IQuery<Account>>
	{
		public AccountQueryObject(IMapper mapper, IQuery<Account> query) : base(mapper, query) { }

		protected override IQuery<Account> ApplyWhereClause(IQuery<Account> query, AccountFilterDto filter)
		{
			if (string.IsNullOrWhiteSpace(filter.Email))
			{
				return query;
			}

			//var accountPredicates = new List<IPredicate>()
			//{
			//	new SimplePredicate(nameof(Account.Email), ValueComparingOperator.Equal, filter.Email),
			//	new SimplePredicate(nameof(Account.Password), ValueComparingOperator.Equal, filter.Password)
			//};
			//var predicate = new CompositePredicate(accountPredicates, LogicalOperator.OR);

			return query.Where(new SimplePredicate(nameof(Account.Email), ValueComparingOperator.Equal, filter.Email));
		}
	}
}
