using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;

namespace AuctionPortal.DataAccessLayer.EntityFramework
{
	public class DatabaseInitializer : DropCreateDatabaseAlways<AuctionPortalDbContext>
	{
		protected override void Seed(AuctionPortalDbContext context)
		{
			base.Seed(context);
		}
	}
}