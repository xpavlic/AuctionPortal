using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.Facades;
using X.PagedList;

namespace AuctionPortal.PresentationLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminAccountsController : Controller
    {
        public AccountFacade AccountFacade { get; set; }

        //public OrderFacade OrderFacade { get; set; }

        public async Task<ActionResult> Index(int page = 1)
        {
            //TODO: very inefficient, why and how could we solve this?
            var result = await AccountFacade.GetAllAccountsAsync();
            var pageSize = result.TotalItemsCount > 0 ? (int)result.TotalItemsCount : 1;
            var model = new StaticPagedList<AccountDTO>(result.Items, page, pageSize, (int)result.TotalItemsCount);
            return View(model);
        }

        //public async Task<ActionResult> Orders(Guid id)
        //{
        //	var orders = await OrderFacade.GetOrdersAsync(new OrderFilterDto { CustomerId = id });
        //	return View(orders);
        //}

        //public async Task<ActionResult> Details(Guid id)
        //{
        //	var orderItems = await OrderFacade.ListItemsFromOrderAsync(new OrderItemFilterDto { OrderId = id });
        //	return View(orderItems);
        //}
    }
}