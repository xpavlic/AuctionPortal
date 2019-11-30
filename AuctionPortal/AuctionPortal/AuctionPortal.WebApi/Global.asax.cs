using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AuctionPortal.BusinessLayer.Config;
using AuctionPortal.WebApi.App_Start.Windsor;
using Castle.Windsor;

namespace AuctionPortal.WebApi
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		private readonly IWindsorContainer container = new WindsorContainer();

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		private void BootstrapContainer()
		{
			container.Install(new WebApiInstaller());
			container.Install(new BusinessLayerInstaller());

			GlobalConfiguration.Configuration.Services
				.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
		}

		public override void Dispose()
		{
			container.Dispose();
			base.Dispose();
		}
	}
}
