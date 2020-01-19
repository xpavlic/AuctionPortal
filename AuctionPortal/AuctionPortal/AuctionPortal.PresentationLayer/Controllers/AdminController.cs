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
    }
}