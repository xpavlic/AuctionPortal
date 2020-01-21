using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.Facades;
using AuctionPortal.PresentationLayer.Models.Admin;

namespace AuctionPortal.PresentationLayer.Controllers
{
	public class AdminController : Controller
	{
		public AccountFacade AccountFacade { get; set; }

		public AuctionFacade AuctionFacade { get; set; }


		public async Task<ActionResult> AccountList()
		{
			var allAccounts = await AccountFacade.GetAllAccountsAsync();
			var model = await InitializeAccountListModel(allAccounts.Items.OrderBy(x => x.FirstName).ToList());
			return View(model);
		}

		public async Task<ActionResult> AuctionList()
		{
			var allAuctions = await AuctionFacade.GetAllAuctionsAsync(new AuctionFilterDto());
			var allCategories = await AuctionFacade.GetAllCategories();
			var model = await InitializeAuctionListModel(allAuctions.Items.OrderBy(x => x.Name).ToList(),
				allCategories.OrderBy(x => x.Name).ToList());
			return View(model);
		}
		public async Task<ActionResult> Categories()
		{
			var allCategories = (await AuctionFacade.GetAllCategories()).OrderBy(x => x.Name).ToList();
			var model = await InitializeCategoriesModel(allCategories);
			return View(model);
		}

		public async Task<ActionResult> AccountDetail(Guid id)
		{
			var result = await AccountFacade.GetAccountAccordingToIdAsync(id);
			var model = await InitializeAccountEditModel(result);
			return View("AccountEdit", model);
		}

		[HttpPost]
		public async Task<ActionResult> EditAccount(AccountEditModel model)
		{
			var account = await AccountFacade.GetAccountAccordingToIdAsync(model.AccountId);

			account.FirstName = model.FirstName;
			account.LastName = model.LastName;
			account.Email = model.Email;
			account.Password = model.Password;
			account.Address = model.Address;
			account.MobilePhoneNumber = model.MobilePhoneNumber;

			await AccountFacade.EditAccountAsync(account);
			return RedirectToAction("AccountDetail", new { id = model.AccountId });
		}

		[HttpPost]
		public async Task<ActionResult> DeleteAccount(Guid accountId)
		{
			await AccountFacade.DeleteAccountAsync(accountId);
			return RedirectToAction("AccountList");
		}

		private async Task<AccountEditModel> InitializeAccountEditModel(AccountDTO account)
		{
			var auctions = (await AuctionFacade.GetAllAuctionsForAccount(account.Id)).OrderBy(x => x.Name).ToList();
			var bids = (await AccountFacade.GetAllBidsAccordingToAccount(account.Id)).OrderBy(x => x.BidDateTime).ToList();

			return new AccountEditModel
			{
				FirstName = account.FirstName,
				LastName = account.LastName,
				Email = account.Email,
				Password = account.Password,
				Address = account.Address,
				MobilePhoneNumber = account.MobilePhoneNumber,
				BirthDate = account.BirthDate,
				Auctions = auctions,
				Bids = bids,
				AccountId = account.Id
			};
		}

		private async Task<AccountListModel> InitializeAccountListModel(IList<AccountDTO> accountsList)
		{
			return new AccountListModel()
			{
				Accounts = accountsList
			};
		}
		private async Task<AuctionListModel> InitializeAuctionListModel(List<AuctionDTO> auctionsList, List<CategoryDTO> categoriesList)
		{
			return new AuctionListModel()
			{
				Auctions = auctionsList,
				Categories = categoriesList
			};
		}

		private async Task<CategoriesModel> InitializeCategoriesModel(List<CategoryDTO> categoriesList)
		{
			return new CategoriesModel()
			{
				Categories = categoriesList,
				NewCategoryName = "NewCategoryName"
			};
		}

		[HttpPost]
		public async Task<ActionResult> CreateCategory(CategoriesModel categoriesModel)
		{
			var category = new CategoryDTO()
			{
				Id = Guid.NewGuid(),
				Name = categoriesModel.NewCategoryName,
				Parent = null,
				ParentId = null
			};

			var result = await AuctionFacade.CreateCategory(category);
			return RedirectToAction("Categories");
		}

		[HttpPost]
		public ActionResult DeleteCategory(Guid categoryid)
		{
			AuctionFacade.DeleteCategory(categoryid);
			return RedirectToAction("Categories");
		}
	}
}