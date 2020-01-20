using System.Web.Mvc;

namespace AuctionPortal.PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
		[AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Auction portal description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contact page.";

            return View();
        }
    }
}