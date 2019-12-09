﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.Facades;
using AuctionPortal.PresentationLayer.Models.Auctions;
using X.PagedList;

namespace AuctionPortal.PresentationLayer.Controllers
{
    public class AuctionsController : Controller
    {
		public const int PageSize = 9;

		private const string FilterSessionKey = "filter";
		private const string CategoryTreesSessionKey = "categoryTrees";

		public AuctionFacade AuctionFacade { get; set; }

		public async Task<ActionResult> Index(int page = 1)
		{
			var filter = Session[FilterSessionKey] as AuctionFilterDto ?? new AuctionFilterDto { PageSize = PageSize };
			filter.RequestedPageNumber = page;

			//TODO: This is soo much inefficient, why and how could we solve this?
			var allAuctions = await AuctionFacade.GetAllAuctionsAsync(new AuctionFilterDto());
			var result = await AuctionFacade.GetAllAuctionsAsync(filter);

			var categoryTrees = Session[CategoryTreesSessionKey] as IList<CategoryDTO>;
			var model = await InitializeAuctionListViewModel(result, (int)allAuctions.TotalItemsCount, categoryTrees);
			return View("AuctionListView", model);
		}

		[HttpPost]
		public async Task<ActionResult> Index(AuctionListViewModel model)
		{
			model.Filter.PageSize = PageSize;
			model.Filter.CategoryIds = ProcessCategoryIds(model);
			Session[FilterSessionKey] = model.Filter;
			Session[CategoryTreesSessionKey] = model.Categories;

			//TODO: This is soo much inefficient, why and how could we solve this?
			var allAuctions = await AuctionFacade.GetAllAuctionsAsync(new AuctionFilterDto());
			var result = await AuctionFacade.GetAllAuctionsAsync(model.Filter);
			var newModel = await InitializeAuctionListViewModel(result, (int)allAuctions.TotalItemsCount, model.Categories);
			return View("AuctionListView", newModel);
		}

		public ActionResult ClearFilter()
		{
			Session[FilterSessionKey] = null;
			Session[CategoryTreesSessionKey] = null;
			return RedirectToAction(nameof(Index));
		}

		public async Task<ActionResult> Details(Guid id)
		{
			var model = await AuctionFacade.GetAuctionAsync(id);
			return View("AuctionDetailView", model);
		}

		private async Task<AuctionListViewModel> InitializeAuctionListViewModel(QueryResultDto<AuctionDTO, AuctionFilterDto> result, int totalItemsCount, IList<CategoryDTO> categories = null)
		{
			return new AuctionListViewModel
			{
				Auctions = new StaticPagedList<AuctionDTO>(result.Items, result.RequestedPageNumber ?? 1, PageSize, totalItemsCount),
				Categories = categories ?? await AuctionFacade.GetAllCategories() as IList<CategoryDTO>,
				Filter = result.Filter
			};
		}

		private static Guid[] ProcessCategoryIds(AuctionListViewModel model)
		{
			var selectedCategoryIds = new List<Guid>();
			foreach (var categoryTreeRoot in model.Categories)
			{
				selectedCategoryIds.Add(categoryTreeRoot.Id);
				selectedCategoryIds.AddRange(model.Categories
					.Where(node => node.ParentId == categoryTreeRoot.Id)
					.Select(node => node.Id));
			}
			return selectedCategoryIds.ToArray();
		}
	}
}