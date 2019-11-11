using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure;
using AuctionPortal.Infrastructure.EntityFramework;
using AuctionPortal.Infrastructure.EntityFramework.UnitOfWork;
using AuctionPortal.Infrastructure.Query;
using AuctionPortal.Infrastructure.UnitOfWork;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Tests.Config
{
	public class EntityFrameworkTestInstaller : IWindsorInstaller
	{
		private const string TestDbConnectionString = "InMemoryTestDBAuctionPortal";

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<Func<DbContext>>()
					.Instance(InitializeDatabase)
					.LifestyleTransient(),
				Component.For<IUnitOfWorkProvider>()
					.ImplementedBy<EntityFrameworkUnitOfWorkProvider>()
					.LifestyleSingleton(),
				Component.For(typeof(IRepository<>))
					.ImplementedBy(typeof(EntityFrameworkRepository<>))
					.LifestyleTransient(),
				Component.For(typeof(IQuery<>))
					.ImplementedBy(typeof(EntityFrameworkQuery<>))
					.LifestyleTransient()
			);
		}

		private static DbContext InitializeDatabase()
		{
			var context = new AuctionPortalDbContext();
			context.Products.RemoveRange(context.Products);
			context.Categories.RemoveRange(context.Categories);
			context.SaveChanges();

			var vehicles = new Category
			{
				Id = Guid.Parse("aa01dc64-5c07-40fe-a916-175165b9b90f"),
				Name = "Vehicles",
				Parent = null,
				ParentId = null
			};

			var skoda = new Category
			{
				Id = Guid.Parse("aa02dc64-5c07-40fe-a916-175165b9b90f"),
				Name = "Skoda",
				Parent = vehicles,
				ParentId = vehicles.Id
			};

			var audi = new Category
			{
				Id = Guid.Parse("aa04dc64-5c07-40fe-a916-175165b9b90f"),
				Name = "Audi",
				Parent = vehicles,
				ParentId = vehicles.Id
			};

			context.Categories.AddOrUpdate(category => category.Id, vehicles, skoda, audi);


			var kodiaqAuction = new Auction
			{
				Id = Guid.Parse("aa05dc64-5c07-40fe-a916-175165b9b90f"),
				Category = skoda,
				CategoryId = skoda.Id,
				Description = "The ŠKODA KODIAQ, a large 4.70 metre long SUV boasting up to seven seats and one of the largest luggage compartments in its class, provides oodles of space even for the most demanding families.",
				Name = "Skoda Kodiaq Auction",
				ActualPrice = 800_000,
				ClosingTime = new DateTime(2020, 1, 1),
				IsOpened = true,
			};

			var a6Auction = new Auction
			{
				Id = Guid.Parse("aa06dc64-5c07-40fe-a916-175165b9b90f"),
				Category = audi,
				CategoryId = audi.Id,
				Description = "The Audi A6 is an executive car made by the German automaker Audi, now in its fifth generation. ... All generations of the A6 have offered either front-wheel drive or Torsen-based four-wheel drive, marketed by Audi as their quattro system.",
				Name = "Audi A6 Auction",
				ActualPrice = 1_200_000,
				ClosingTime = new DateTime(2020, 2, 1),
				IsOpened = true,
			};

			context.Auctions.AddOrUpdate(kodiaqAuction);
			context.Auctions.AddOrUpdate(a6Auction);

			context.SaveChanges();

			return context;
		}
	}
}
