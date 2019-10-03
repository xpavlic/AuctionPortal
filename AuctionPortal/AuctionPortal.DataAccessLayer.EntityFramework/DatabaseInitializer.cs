using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;

namespace AuctionPortal.DataAccessLayer.EntityFramework
{
	public class DatabaseInitializer : DropCreateDatabaseAlways<AuctionPortalDbContext>
	{
		protected override void Seed(AuctionPortalDbContext context)
		{
			var product = new Product { Id = Guid.Parse("aa01dc64-5c07-40fe-a916-175165b9b90f"), Name = "Cat",
				ProductImgUrls = 
				new List<string>(){@"\Content\Images\Products\samsung_galaxy_J7.jpeg"}
			};
			context.Products.AddOrUpdate(product);
			context.SaveChanges();
			base.Seed(context);
		}
	}
}
