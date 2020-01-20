using AuctionPortal.BusinessLayer.Facades.Common;
using AuctionPortal.BusinessLayer.QueryObjects.Common;
using AuctionPortal.BusinessLayer.Services.Common;
using AuctionPortal.DataAccessLayer.EntityFramework.Config;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Component = Castle.MicroKernel.Registration.Component;

namespace AuctionPortal.BusinessLayer.Config
{
	public class BusinessLayerInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			new EntityFrameworkInstaller().Install(container, store);

			container.Register(
				Classes.FromThisAssembly()
					.BasedOn(typeof(QueryObjectBase<,,,>))
					.WithServiceBase()
					.LifestyleTransient(),

				Classes.FromThisAssembly()
					.BasedOn<ServiceBase>()
					.WithServiceDefaultInterfaces()
					.LifestyleTransient(),

				Classes.FromThisAssembly()
					.BasedOn<FacadeBase>()
					.LifestyleTransient(),

				Component.For<IMapper>()
					.Instance(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)))
					.LifestyleSingleton()
			);
		}
	}
}