using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.Facades;
using AuctionPortal.PresentationLayer.Models.AdminAuctions;
using X.PagedList;

namespace AuctionPortal.PresentationLayer.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	public class AdminAuctionsController : Controller
	{
		public AuctionFacade AuctionFacade { get; set; }

		// GET: AdminProducts
		public async Task<ActionResult> Index(int page = 1)
		{
			//TODO: very inefficient, why and how could we solve this?
			var result = await AuctionFacade.GetAllAuctionsAsync(new AuctionFilterDto());
			var pageSize = result.TotalItemsCount > 0 ? (int)result.TotalItemsCount : 1;
			var model = new StaticPagedList<AuctionDTO>(result.Items, page, pageSize, (int)result.TotalItemsCount);
			return View(model);
		}

		// GET: AdminProducts/Details/<Guid>
		public async Task<ActionResult> Details(Guid id)
		{
			var model = await AuctionFacade.GetAuctionAsync(id);
			return View(model);
		}

		// GET: AdminProducts/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: AdminProducts/Create
		[HttpPost]
		public async Task<ActionResult> Create(AdminAuctionEditModel model)
		{
			try
			{
				await AuctionFacade.CreateAuctionWithCategoryNameAsync(model.Auction, model.Category.Name);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: AdminProducts/Edit/5
		public async Task<ActionResult> Edit(Guid id)
		{
			var auction = await AuctionFacade.GetAuctionAsync(id);
			var category = await AuctionFacade.GetCategoryAsync(auction.CategoryId);
			var model = new AdminAuctionEditModel
			{
				Auction = auction,
				Category = category
			};
			return View(model);
		}

		// POST: AdminProducts/Edit/5
		[HttpPost]
		public async Task<ActionResult> Edit(Guid id, AdminAuctionEditModel model)
		{
			try
			{
				model.Auction.CategoryId = (await AuctionFacade.GetProductCategoryIdsByNamesAsync(model.Category.Name)).SingleOrDefault();
				await AuctionFacade.EditAuctionAsync(model.Auction);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: AdminProducts/Delete/5
		public async Task<ActionResult> Delete(Guid id)
		{
			await AuctionFacade.DeleteAuctionAsync(id);
			return RedirectToAction("Index");
		}
	}
}