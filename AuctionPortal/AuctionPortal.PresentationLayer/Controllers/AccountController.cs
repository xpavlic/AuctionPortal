using System.Web.Mvc;

namespace AuctionPortal.PresentationLayer.Controllers
{
	public class AccountController : Controller
	{
		public ActionResult Logout()
		{
			throw new System.NotImplementedException();
		}

		public ActionResult Register()
		{
			return View();
		}

		public ActionResult Login()
		{
			throw new System.NotImplementedException();
		}
	}
}