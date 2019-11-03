﻿using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
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
			throw new NotImplementedException();
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

			var kodiaq = new Product
			{
				Id = Guid.Parse("defdf092-ec30-489d-899b-43d4fda72098"),
				Name = "Skoda Kodiaq",
				ProductImgUrl = @"\Content\Images\Products\skoda_kodiaq.jpeg"
			};

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
				Product = kodiaq,
				ProductId = kodiaq.Id
			};

			kodiaq.Auction = kodiaqAuction;
			kodiaq.AuctionId = kodiaqAuction.Id;

			var a6 = new Product
			{
				Id = Guid.Parse("4294129a-be0d-44dc-973c-488b506b2245"),
				Name = "Audi A6",
				ProductImgUrl = @"\Content\Images\Products\audi_a6.jpg"
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
				Product = a6,
				ProductId = a6.Id
			};

			a6.Auction = a6Auction;
			a6.AuctionId = a6Auction.Id;

			context.Auctions.AddOrUpdate(kodiaqAuction);
			context.Auctions.AddOrUpdate(a6Auction);
			context.Products.AddOrUpdate(kodiaq);
			context.Products.AddOrUpdate(a6);

			context.SaveChanges();

			return context;
		}
	}
}