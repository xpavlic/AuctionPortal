using System;
using System.Collections.Generic;
using System.Linq;
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
			var definedPredicates = new List<IPredicate>();
			AddIfDefined(FilterEmail(filter), definedPredicates);
			AddIfDefined(FilterId(filter), definedPredicates);

			if (definedPredicates.Count == 0)
			{
				return query;
			}

			if (definedPredicates.Count == 1)
			{
				return query.Where(definedPredicates.First());
			}

			var predicate = new CompositePredicate(definedPredicates);
			return query.Where(predicate);
		}

		private static void AddIfDefined(IPredicate accountPredicate, ICollection<IPredicate> definedPredicates)
		{
			if (accountPredicate != null)
			{
				definedPredicates.Add(accountPredicate);
			}
		}

		private static SimplePredicate FilterEmail(AccountFilterDto filter)
		{
			if (string.IsNullOrWhiteSpace(filter.Email))
			{
				return null;
			}
			return new SimplePredicate(nameof(Account.Email), ValueComparingOperator.Equal, filter.Email);
		}

		private static SimplePredicate FilterId(AccountFilterDto filter)
		{
			if (filter.Id.Equals(Guid.Empty))
			{
				return null;
			}
			return new SimplePredicate(nameof(Account.Id), ValueComparingOperator.Equal, filter.Id);
		}
	}
}