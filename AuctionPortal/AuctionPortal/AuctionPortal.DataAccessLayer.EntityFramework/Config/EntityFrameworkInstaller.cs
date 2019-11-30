using System;
using System.Data.Entity;
using AuctionPortal.Infrastructure;
using AuctionPortal.Infrastructure.EntityFramework;
using AuctionPortal.Infrastructure.EntityFramework.UnitOfWork;
using AuctionPortal.Infrastructure.Query;
using AuctionPortal.Infrastructure.UnitOfWork;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Component = Castle.MicroKernel.Registration.Component;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Config
{
	public class EntityFrameworkInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<Func<DbContext>>()
					.Instance(() => new AuctionPortalDbContext())
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
	}
}
