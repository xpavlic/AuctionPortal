using System;
using System.Collections.Generic;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.QueryObjects.Common;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure.Query;
using AuctionPortal.Infrastructure.Query.Predicates;
using AuctionPortal.Infrastructure.Query.Predicates.Operators;
using AutoMapper;

namespace AuctionPortal.BusinessLayer.DataTransferObjects.QueryObjects
{
	public class ProductQueryObject : QueryObjectBase<ProductDTO, Product, ProductFilterDto, IQuery<Product>>
	{
		public ProductQueryObject(IMapper mapper, IQuery<Product> query) : base(mapper, query) { }

		protected override IQuery<Product> ApplyWhereClause(IQuery<Product> query, ProductFilterDto filter)
		{
			return filter.AuctionId.Equals(Guid.Empty) || string.IsNullOrWhiteSpace(filter.Name)
				? query
				: query.Where(new CompositePredicate(new List<IPredicate>()
				{
					new SimplePredicate(nameof(Product.AuctionId), ValueComparingOperator.Equal, filter.AuctionId),
					new SimplePredicate(nameof(Product.Name), ValueComparingOperator.Equal, filter.Name)
				}));
		}
	}
}