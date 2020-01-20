using Castle.Windsor;
using NUnit.Framework;
using System.Data.Entity;
using AuctionPortal.DataAccessLayer.EntityFramework.Tests.Config;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Tests
{
	[SetUpFixture]
	public class Initializer
	{
		internal static readonly IWindsorContainer Container = new WindsorContainer();

		/// <summary>
		/// Initializes all Business Layer tests
		/// </summary>
		[OneTimeSetUp]
		public void InitializeBusinessLayerTests()
		{
			Database.SetInitializer(new DropCreateDatabaseAlways<AuctionPortalDbContext>());
			Container.Install(new EntityFrameworkTestInstaller());
		}
	}
}
