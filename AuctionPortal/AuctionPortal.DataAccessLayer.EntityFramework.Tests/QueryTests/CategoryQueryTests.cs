using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure.Query;
using AuctionPortal.Infrastructure.Query.Predicates;
using AuctionPortal.Infrastructure.Query.Predicates.Operators;
using AuctionPortal.Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Tests.QueryTests
{
	[TestFixture]
	public class CategoryQueryTests
	{
		private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

		private readonly Guid? vehiclesCategoryId = Guid.Parse("aa01dc64-5c07-40fe-a916-175165b9b90f");
		private readonly Guid skodaCategoryId = Guid.Parse("aa02dc64-5c07-40fe-a916-175165b9b90f");

		private readonly Guid audiCategoryId = Guid.Parse("aa04dc64-5c07-40fe-a916-175165b9b90f");

		[Test]
		public async Task ExecuteAsync_SimpleWherePredicate_ReturnsCorrectQueryResult()
		{
			QueryResult<Category> actualQueryResult;
			var categoryQuery = Initializer.Container.Resolve<IQuery<Category>>();
			var expectedQueryResult = new QueryResult<Category>(new List<Category>{new Category
			{
				Id = skodaCategoryId, Name = "Skoda", ParentId = vehiclesCategoryId
			}, new Category
			{
				Id = audiCategoryId, Name = "Audi", ParentId = vehiclesCategoryId
			}}, 2);

			var predicate = new SimplePredicate(nameof(Category.ParentId), ValueComparingOperator.Equal, vehiclesCategoryId);
			using (unitOfWorkProvider.Create())
			{
				actualQueryResult = await categoryQuery.Where(predicate).ExecuteAsync();
			}

			Assert.AreEqual(actualQueryResult, expectedQueryResult);
		}

		[Test]
		public async Task ExecuteAsync_ComplexWherePredicate_ReturnsCorrectQueryResult()
		{
			QueryResult<Category> actualQueryResult;
			var categoryQuery = Initializer.Container.Resolve<IQuery<Category>>();
			var expectedQueryResult = new QueryResult<Category>(new List<Category>{new Category
			{
				Id = skodaCategoryId, Name = "Skoda", ParentId = vehiclesCategoryId
			}}, 1);

			var predicate = new CompositePredicate(new List<IPredicate>
			{
				new SimplePredicate(nameof(Category.ParentId), ValueComparingOperator.Equal, vehiclesCategoryId),
				new CompositePredicate(new List<IPredicate>
				{
					new SimplePredicate(nameof(Category.Name), ValueComparingOperator.Equal, "Skoda"),
					new SimplePredicate(nameof(Category.Name), ValueComparingOperator.Equal, "Renault")
				}, LogicalOperator.OR)
			});
			using (unitOfWorkProvider.Create())
			{
				actualQueryResult = await categoryQuery.Where(predicate).ExecuteAsync();
			}

			Assert.AreEqual(actualQueryResult, expectedQueryResult);
		}

		[Test]
		public async Task ExecuteAsync_OrderAllCategoriesByName_ReturnsCorrectlyOrderedQueryResult()
		{
			QueryResult<Category> actualQueryResult;
			var categoryQuery = Initializer.Container.Resolve<IQuery<Category>>();
			var expectedQueryResult = new QueryResult<Category>(new List<Category>{new Category
			{
				Id = audiCategoryId, Name = "Audi", ParentId = vehiclesCategoryId
			}, new Category
			{
				Id = skodaCategoryId, Name = "Skoda", ParentId = vehiclesCategoryId
			}, new Category
			{
				Id = vehiclesCategoryId.Value, Name = "Vehicles", ParentId = null
			}}, 3);

			using (unitOfWorkProvider.Create())
			{
				actualQueryResult = await categoryQuery.SortBy(nameof(Category.Name), true).ExecuteAsync();
			}

			Assert.AreEqual(actualQueryResult, expectedQueryResult);
		}

		[Test]
		public async Task ExecuteAsync_RetrieveSecondCategoriesPage_ReturnsCorrectPage()
		{
			const int pageSize = 2;
			const int requestedPage = 2;
			QueryResult<Category> actualQueryResult;
			var categoryQuery = Initializer.Container.Resolve<IQuery<Category>>();
			var expectedQueryResult = new QueryResult<Category>(new List<Category>{new Category
			{
				Id = audiCategoryId, Name = "Audi", ParentId = vehiclesCategoryId
			}}, 1, pageSize, requestedPage);

			using (unitOfWorkProvider.Create())
			{
				actualQueryResult = await categoryQuery.Page(requestedPage, pageSize).ExecuteAsync();
			}

			Assert.AreEqual(actualQueryResult, expectedQueryResult);
		}
	}
}
