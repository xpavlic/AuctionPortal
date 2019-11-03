using System;
using System.Threading.Tasks;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure;
using AuctionPortal.Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Tests.RepositoryTests
{
	[TestFixture]
	public class AuctionRepositoryTests
	{
		private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

		private readonly IRepository<Auction> auctionRepository = Initializer.Container.Resolve<IRepository<Auction>>();

		private readonly Guid skodaCategoryId = Guid.Parse("aa02dc64-5c07-40fe-a916-175165b9b90f");

		private readonly Guid kodiaqAuctionId = Guid.Parse("aa05dc64-5c07-40fe-a916-175165b9b90f");

		[Test]
		public async Task GetAuctionAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectAuction()
		{
			Auction kodiaqAuction;

			using (unitOfWorkProvider.Create())
			{
				kodiaqAuction = await auctionRepository.GetAsync(kodiaqAuctionId);
			}

			Assert.AreEqual(kodiaqAuction.Id, kodiaqAuctionId);
		}

		[Test]
		public async Task GetAuctionAsync_AlreadyStoredInDBWithIncludes_ReturnsCorrectAuctionWithInitializedParent()
		{
			Auction kodiaqAuction;

			using (unitOfWorkProvider.Create())
			{
				kodiaqAuction = await auctionRepository.GetAsync(kodiaqAuctionId, nameof(Auction.Category));
			}

			Assert.IsTrue(kodiaqAuction.Id.Equals(kodiaqAuctionId) && kodiaqAuction.Category.Id.Equals(skodaCategoryId));
		}

		[Test]
		public async Task CreateAuctionAsync_AuctionIsNotPreviouslySeeded_CreatesNewAuction()
		{
			var karoq = new Product
			{
				Id = Guid.Parse("defdf092-ec30-489d-899b-43d4fda72098"),
				Name = "Skoda Karoq",
				ProductImgUrl = @"\Content\Images\Products\skoda_karoq.jpeg"
			};

			var karoqAuction = new Auction
			{
				CategoryId = skodaCategoryId,
				Description = "The Škoda Karoq is a compact crossover SUV (J-segment designed and built by the Czech car manufacturer Škoda Auto. The vehicle is based on the VW MQB platform, and officially replaced the Škoda Yeti. It was revealed in May 2017, and the sales began in the second half of 2017. It is a sister model to the SEAT Ateca.",
				Name = "Skoda Karoq Auction",
				ActualPrice = 600_000,
				ClosingTime = new DateTime(2020, 1, 15),
				IsOpened = true,
				Product = karoq,
				ProductId = karoq.Id
			};

			using (var uow = unitOfWorkProvider.Create())
			{
				auctionRepository.Create(karoqAuction);
				await uow.Commit();
			}
			Assert.IsTrue(!karoqAuction.Id.Equals(Guid.Empty));
		}

		[Test]
		public async Task DeleteAuctionAsync_AuctionIsPreviouslySeeded_DeletesAuction()
		{
			Auction deletedSkodaAuction;

			using (var uow = unitOfWorkProvider.Create())
			{
				auctionRepository.Delete(kodiaqAuctionId);
				await uow.Commit();
				deletedSkodaAuction = await auctionRepository.GetAsync(kodiaqAuctionId);
			}

			Assert.AreEqual(deletedSkodaAuction, null);
		}

	}
}
