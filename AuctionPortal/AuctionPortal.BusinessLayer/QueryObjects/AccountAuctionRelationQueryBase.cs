﻿using System;
using System.Collections.Generic;
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
	public class AccountAuctionRelationQueryBase : 
		QueryObjectBase<AccountAuctionRelationDTO, AccountAuctionRelation, AccountAuctionRelationFilterDto, IQuery<AccountAuctionRelation>>
	{
		public AccountAuctionRelationQueryBase(IMapper mapper, IQuery<AccountAuctionRelation> query) : base(mapper, query) { }

		protected override IQuery<AccountAuctionRelation> ApplyWhereClause(IQuery<AccountAuctionRelation> query, AccountAuctionRelationFilterDto filter)
		{
			if (filter.AuctionId.Equals(Guid.Empty) || filter.AccountId.Equals(Guid.Empty))
			{
				return query;
			}

			var accountAuctionRelationPredicates = new List<IPredicate>()
			{
				new SimplePredicate(nameof(AccountAuctionRelation.AuctionId), ValueComparingOperator.Equal, filter.AuctionId),
				new SimplePredicate(nameof(AccountAuctionRelation.AccountId), ValueComparingOperator.Equal, filter.AccountId)
			};
			var predicate = new CompositePredicate(accountAuctionRelationPredicates);
			return query.Where(predicate);
		}
	}
}