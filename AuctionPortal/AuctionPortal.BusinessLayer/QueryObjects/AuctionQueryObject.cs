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
	public class AuctionQueryObject : QueryObjectBase<AuctionDTO, Auction, AuctionFilterDto, IQuery<Auction>>
	{
		public AuctionQueryObject(IMapper mapper, IQuery<Auction> query) : base(mapper, query) { }

		protected override IQuery<Auction> ApplyWhereClause(IQuery<Auction> query, AuctionFilterDto filter)
		{
			var definedPredicates = new List<IPredicate>();
			AddIfDefined(FilterCategories(filter), definedPredicates);
			AddIfDefined(FilterName(filter), definedPredicates);
			AddIfDefined(FilterProductId(filter), definedPredicates);
			AddIfDefined(FilterClosingTime(filter), definedPredicates);
			AddIfDefined(FilterActualPrice(filter), definedPredicates);

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

		private static void AddIfDefined(IPredicate auctionPredicate, ICollection<IPredicate> definedPredicates)
		{
			if (auctionPredicate != null)
			{
				definedPredicates.Add(auctionPredicate);
			}
		}

		private static CompositePredicate FilterCategories(AuctionFilterDto filter)
		{
			if (filter.CategoryIds == null || !filter.CategoryIds.Any())
			{
				return null;
			}
			var categoryIdPredicates = new List<IPredicate>(filter.CategoryIds
				.Select(categoryId => new SimplePredicate(
					nameof(Auction.CategoryId),
					ValueComparingOperator.Equal,
					categoryId)));
			return new CompositePredicate(categoryIdPredicates, LogicalOperator.OR);
		}

		private static SimplePredicate FilterName(AuctionFilterDto filter)
		{
			if (string.IsNullOrWhiteSpace(filter.Name))
			{
				return null;
			}
			return new SimplePredicate(nameof(Auction.Name), ValueComparingOperator.Equal, filter.Name);
		}

		private static SimplePredicate FilterProductId(AuctionFilterDto filter)
		{
			if (filter.ProductId.Equals(Guid.Empty))
			{
				return null;
			}
			return new SimplePredicate(nameof(Auction.ProductId), ValueComparingOperator.Equal, filter.ProductId);
		}

		private static SimplePredicate FilterClosingTime(AuctionFilterDto filter)
		{
			if (filter.ClosingTime < DateTime.Now)
			{
				return null;
			}
			return new SimplePredicate(nameof(Auction.ClosingTime), ValueComparingOperator.Equal, filter.ClosingTime);
		}

		private static SimplePredicate FilterActualPrice(AuctionFilterDto filter)
		{
			if (filter.ActualPrice < 0 || filter.ActualPrice == decimal.MaxValue)
			{
				return null;
			}
			return new SimplePredicate(nameof(Auction.ActualPrice), ValueComparingOperator.Equal, filter.ActualPrice);
		}
	}
}
