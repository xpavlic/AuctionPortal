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
	public class CategoryQueryObject : QueryObjectBase<CategoryDTO, Category, CategoryFilterDto, IQuery<Category>>
	{
		public CategoryQueryObject(IMapper mapper, IQuery<Category> query) : base(mapper, query) { }

		protected override IQuery<Category> ApplyWhereClause(IQuery<Category> query, CategoryFilterDto filter)
		{
			if (filter.Names == null || !filter.Names.Any())
			{
				return query;
			}

			var categoryNamePredicates = new List<IPredicate>(filter.Names
				.Select(name => new SimplePredicate(
					nameof(Category.Name),
					ValueComparingOperator.Equal,
					name)));
			var predicate = new CompositePredicate(categoryNamePredicates, LogicalOperator.OR);
			return query.Where(predicate);
		}
	}
}
