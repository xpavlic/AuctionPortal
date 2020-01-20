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
	public class AuctionQueryTests
	{
		private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

		private readonly Guid skodaCategoryId = Guid.Parse("aa02dc64-5c07-40fe-a916-175165b9b90f");
		private readonly Guid audiCategoryId = Guid.Parse("aa04dc64-5c07-40fe-a916-175165b9b90f");

		private readonly Guid kodiaqAuction = Guid.Parse("aa05dc64-5c07-40fe-a916-175165b9b90f");
		private readonly Guid a6Auction = Guid.Parse("aa06dc64-5c07-40fe-a916-175165b9b90f");

		[Test]
		public async Task ExecuteAsync_SimpleWherePredicate_ReturnsCorrectQueryResult()
		{
			QueryResult<Auction> actualQueryResult;
			var auctionQuery = Initializer.Container.Resolve<IQuery<Auction>>();

			var expectedQueryResult = new QueryResult<Auction>(new List<Auction> { new Auction
			{
				Id = kodiaqAuction,
				CategoryId = skodaCategoryId,
				Description = "The ŠKODA KODIAQ, a large 4.70 metre long SUV boasting up to seven seats and one of the largest luggage compartments in its class, provides oodles of space even for the most demanding families.",
				Name = "Skoda Kodiaq Auction",
				ActualPrice = 800_000,
				ClosingTime = new DateTime(2020, 1, 1),
				IsOpened = true
			}}, 1);

			var predicate = new SimplePredicate(nameof(Auction.CategoryId), ValueComparingOperator.Equal, skodaCategoryId);
			using (unitOfWorkProvider.Create())
			{
				actualQueryResult = await auctionQuery.Where(predicate).ExecuteAsync();
			}

			Assert.AreEqual(actualQueryResult, expectedQueryResult);
		}

		[Test]
		public async Task ExecuteAsync_ComplexWherePredicate_ReturnsCorrectQueryResult()
		{
			QueryResult<Auction> actualQueryResult;
			var auctionQuery = Initializer.Container.Resolve<IQuery<Auction>>();

			var expectedQueryResult = new QueryResult<Auction>(new List<Auction>{new Auction
			{
				Id = kodiaqAuction,
				CategoryId = skodaCategoryId,
				Description = "The ŠKODA KODIAQ, a large 4.70 metre long SUV boasting up to seven seats and one of the largest luggage compartments in its class, provides oodles of space even for the most demanding families.",
				Name = "Skoda Kodiaq Auction",
				ActualPrice = 800_000,
				ClosingTime = new DateTime(2020, 1, 1),
				IsOpened = true
			}}, 1);

			var predicate = new CompositePredicate(new List<IPredicate>
			{
				new SimplePredicate(nameof(Auction.CategoryId), ValueComparingOperator.Equal, skodaCategoryId),
				new CompositePredicate(new List<IPredicate>
				{
					new SimplePredicate(nameof(Auction.ActualPrice), ValueComparingOperator.GreaterThanOrEqual, 200_000),
					new SimplePredicate(nameof(Auction.ActualPrice), ValueComparingOperator.LessThanOrEqual, 800_000)
				})
			});
			using (unitOfWorkProvider.Create())
			{
				actualQueryResult = await auctionQuery.Where(predicate).ExecuteAsync();
			}

			Assert.AreEqual(actualQueryResult, expectedQueryResult);
		}

		[Test]
		public async Task ExecuteAsync_LikeOperator_ReturnsCorrectPage()
		{
			QueryResult<Auction> actualQueryResult;
			var auctionQuery = Initializer.Container.Resolve<IQuery<Auction>>();

			var expectedQueryResult = new QueryResult<Auction>(new List<Auction>{new Auction
			{
				Id = kodiaqAuction,
				CategoryId = skodaCategoryId,
				Description = "The ŠKODA KODIAQ, a large 4.70 metre long SUV boasting up to seven seats and one of the largest luggage compartments in its class, provides oodles of space even for the most demanding families.",
				Name = "Skoda Kodiaq Auction",
				ActualPrice = 800_000,
				ClosingTime = new DateTime(2020, 1, 1),
				IsOpened = true
			}}, 1);

			using (unitOfWorkProvider.Create())
			{
				actualQueryResult = await auctionQuery.Where(new SimplePredicate(nameof(Auction.Name), ValueComparingOperator.StringContains, "Skoda")).ExecuteAsync();
			}

			Assert.AreEqual(actualQueryResult, expectedQueryResult);
		}
	}
}
