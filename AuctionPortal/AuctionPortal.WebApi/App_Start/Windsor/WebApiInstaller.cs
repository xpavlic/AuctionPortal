using Castle.Windsor;
using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;

namespace AuctionPortal.WebApi.App_Start.Windsor
{
	public class WebApiInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Classes.FromThisAssembly()
					.BasedOn<IHttpController>()
					.LifestylePerWebRequest()
			);
		}
	}
}