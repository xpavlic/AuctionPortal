using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.Facades;
using AuctionPortal.PresentationLayer.Models.Auctions;
using Castle.Core;
using X.PagedList;

namespace AuctionPortal.PresentationLayer.Controllers
{
	public class AuctionsController : Controller
	{
		public const int PageSize = 9;

		private const string FilterSessionKey = "filter";
		private const string CategoryTreesSessionKey = "categoryTrees";

		public AuctionFacade AuctionFacade { get; set; }
		public AccountFacade AccountFacade { get; set; }
		public ProductFacade ProductFacade { get; set; }

		public async Task<ActionResult> Index(int page = 1)
		{
			var filter = Session[FilterSessionKey] as AuctionFilterDto ?? new AuctionFilterDto { PageSize = PageSize };
			filter.RequestedPageNumber = page;

			//TODO: This is soo much inefficient, why and how could we solve this?
			var allAuctions = await AuctionFacade.GetAllAuctionsAsync(new AuctionFilterDto());
			var result = await AuctionFacade.GetAllAuctionsAsync(filter);
            var model = await InitializeAuctionListViewModel(result, result.Items, (int)allAuctions.TotalItemsCount);
			return View("AuctionListView", model);
		}

		[HttpPost]
		public async Task<ActionResult> Index(AuctionListViewModel model)
		{
			model.Filter.PageSize = PageSize;
            Session[FilterSessionKey] = model.Filter;

			var allAuctions = await AuctionFacade.GetAllAuctionsAsync(new AuctionFilterDto());
			var result = (await AuctionFacade.GetAllAuctionsAsync(model.Filter));
            var filteredAuctions = model.CategoryId != null ? result.Items.Where(x => x.CategoryId == new Guid(model.CategoryId)) : result.Items;
			var newModel = await InitializeAuctionListViewModel(result, filteredAuctions, (int)allAuctions.TotalItemsCount);
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
			var result = await AuctionFacade.GetAuctionAsync(id);
			var model = await InitializeAuctionDetailViewModel(result);
			return View("AuctionDetailView", model);
		}

		public async Task<ActionResult> Create()
        {
            var categories = await AuctionFacade.GetAllCategories();
			var model = new AuctionCreateViewModel();
			model.CategoriesSelectList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                model.CategoriesSelectList.Add(new SelectListItem { Text=category.Name, Value=category.Id.ToString()});
            }
			return View("AuctionCreateView", model);
		}

		[HttpPost]
		public async Task<ActionResult> Create(AuctionCreateViewModel auctionViewModel)
        {
            var account = await AccountFacade.GetAccountAccordingToEmailAsync(auctionViewModel.AccountEmail);
            var category = await AuctionFacade.GetCategoryAsync(new Guid(auctionViewModel.CategoryId));
            var auctionDto = new AuctionDTO()
            {
                ClosingTime = auctionViewModel.ClosingTime,
                ActualPrice = auctionViewModel.ActualPrice,
                Name = auctionViewModel.Name, 
                Description = auctionViewModel.Description,
				AccountId = account.Id,
				IsOpened = true
            };
            await AuctionFacade.CreateAuctionWithCategoryNameAsync(auctionDto, category.Name);

			return RedirectToAction("Index", "Home");
		}

		private async Task<AuctionDetailViewModel> InitializeAuctionDetailViewModel(AuctionDTO auction)
		{
            var model = new AuctionDetailViewModel {Name = auction.Name};
            var result = await AccountFacade.GetAccountAccordingToIdAsync(auction.AccountId);

			model.Bids = new List<Pair<AccountAuctionRelationDTO, AccountDTO>>();
            var bidsList = (await AuctionFacade.GetAllBidsAccordingToAuction(auction.Id)).ToList().OrderBy(x => x.BidDateTime);
            foreach (var bid in bidsList)
            {
                model.Bids.Add(new Pair<AccountAuctionRelationDTO, AccountDTO>(bid, await AccountFacade.GetAccountAccordingToIdAsync(bid.AccountId)));
            }
			model.AccountFullName = result.FirstName + " " + result.LastName;
			model.Description = auction.Description;
			model.ClosingTime = auction.ClosingTime;
			var products = await ProductFacade.GetAllProductsInAuction(auction.Id);
			model.Products = products.ToList();
			model.ActualPrice = auction.ActualPrice;
			model.IsOpened = auction.IsOpened;
            model.Id = auction.Id;
            model.AuctionOwnerEmail = result.Email;
            model.CategoryId = auction.CategoryId.ToString();
            var categories = await AuctionFacade.GetAllCategories();
            model.CategoriesSelectList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                model.CategoriesSelectList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            }


			return model;
		}

        [HttpPost]
		public async Task<ActionResult> CreateBid(AuctionDetailViewModel auctionModel)
        {
            var accountDto = await AccountFacade.GetAccountAccordingToEmailAsync(auctionModel.EmailOfBidAccount);
            var result = await AuctionFacade.BidOnAuctionAsync(auctionModel.Id, accountDto.Id, auctionModel.NewBidValue);
            return await Details(auctionModel.Id);
        }

		private async Task<AuctionListViewModel> InitializeAuctionListViewModel(QueryResultDto<AuctionDTO, AuctionFilterDto> result, IEnumerable<AuctionDTO> filteredAuctions, int totalItemsCount)
		{
			return new AuctionListViewModel
			{
				Auctions = new StaticPagedList<AuctionDTO>(filteredAuctions, result.RequestedPageNumber ?? 1, PageSize, totalItemsCount),
                Filter = result.Filter,
				CategoriesSelectList = new List<SelectListItem>((await AuctionFacade.GetAllCategories()).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()}))
			};
		}

        public async Task<ActionResult> EditAuctionInit(AuctionDetailViewModel auctionModel)
        {
            var result = await AuctionFacade.GetAuctionAsync(auctionModel.Id);
            var model = await InitializeAuctionDetailViewModel(result);
			return View("AuctionEditView", model);
        }

        public async Task<ActionResult> EditAuction(AuctionDetailViewModel auctionModel)
        {
            var auctionDto = await AuctionFacade.GetAuctionAsync(auctionModel.Id);
            auctionDto.CategoryId = new Guid(auctionModel.CategoryId);
            auctionDto.IsOpened = auctionModel.IsOpened;
            auctionDto.ClosingTime = auctionModel.ClosingTime;
            auctionDto.Description = auctionModel.Description;
            await AuctionFacade.EditAuctionAsync(auctionDto);
            auctionDto = await AuctionFacade.GetAuctionAsync(auctionModel.Id);
            auctionModel = await InitializeAuctionDetailViewModel(auctionDto);
			return View("AuctionDetailView", auctionModel);
        }

        public async Task<ActionResult> DeleteAuction(AuctionDetailViewModel model)
        {
            await AuctionFacade.DeleteAuctionAsync(model.Id);
            return RedirectToAction("Index", "Home");
        }
    }
}