using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.Facades;
using AuctionPortal.BusinessLayer.Services.Accounts;
using AuctionPortal.PresentationLayer.Models.Accounts;
using Castle.Core;

namespace AuctionPortal.PresentationLayer.Controllers
{
	public class AccountController : Controller
	{
		public AccountFacade AccountFacade { get; set; }
		public AuctionFacade AuctionFacade { get; set; }

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Register(AccountCreateDTO accountCreateDto)
		{
			try
			{
				await AccountFacade.RegisterAccount(accountCreateDto);
				//FormsAuthentication.SetAuthCookie(userCreateDto.Username, false);

				var authTicket = new FormsAuthenticationTicket(1, accountCreateDto.Email, DateTime.Now,
					DateTime.Now.AddMinutes(30), false, "");
				string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
				var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
				HttpContext.Response.Cookies.Add(authCookie);

				return RedirectToAction("Index", "Home");
			}
			catch (ArgumentException)
			{
				ModelState.AddModelError("Email Address", "Account with that email address already exists!");
				return View();
			}
			catch (DbEntityValidationException)
			{
                return View();
			}
			catch (SqlException)
			{
                return View();
			}
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Login(LoginModel model, string returnUrl)
		{
			(bool success, bool isAdministrator) = await AccountFacade.Login(model.EmailAddress, model.Password);
			if (success)
			{
				//FormsAuthentication.SetAuthCookie(model.Username, false);

				var authTicket = new FormsAuthenticationTicket(1, model.EmailAddress, DateTime.Now,
					DateTime.Now.AddMinutes(30), false, isAdministrator.ToString());
				string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
				var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
				HttpContext.Response.Cookies.Add(authCookie);

				var decodedUrl = "";
				if (!string.IsNullOrEmpty(returnUrl))
				{
					decodedUrl = Server.UrlDecode(returnUrl);
				}

				if (Url.IsLocalUrl(decodedUrl))
				{
					return Redirect(decodedUrl);
				}
				return RedirectToAction("Index", "Home");
			}
			ModelState.AddModelError("", "Wrong email address or password!");
			return View();
		}

		public async Task<ActionResult> Logout()
        {
            var account = await AccountFacade.GetAccountAccordingToEmailAsync(User.Identity.Name);
			//Response.ClearAllShoppingCartItems(customer.Username);
			//OrderFacade.ReleaseReservations(customer.Id);

			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}

        public async Task<ActionResult> Details(string email)
        {
            var account = await AccountFacade.GetAccountAccordingToEmailAsync(email);
            var myAuctions = (await AuctionFacade.GetAllAuctionsForAccount(account.Id)).ToList();
            var allMyBids = (await AccountFacade.GetAllBidsAccordingToAccount(account.Id)).ToList();
            var biddedAuctions = new List<AuctionDTO>();
            foreach (var auction in allMyBids)
            {
                biddedAuctions.Add(await AuctionFacade.GetAuctionAsync(auction.AuctionId));
            }
			biddedAuctions = biddedAuctions.Distinct().ToList();

            var biddedAuctionsLastBidAccount = new List<Pair<AuctionDTO, AccountDTO>>();

            foreach (var auction in biddedAuctions)
            {
                biddedAuctionsLastBidAccount.Add(new Pair<AuctionDTO, AccountDTO>(auction, await AccountFacade.GetAccountAccordingToIdAsync(
                    (await AuctionFacade.GetAllBidsAccordingToAuction(auction.Id)).OrderByDescending(x => x.BidDateTime)
                    .First().AccountId)));
            }

            /*var asd = biddedAuctions
                .Distinct().Select(async x =>
                {
                    return new Pair<AuctionDTO, AccountDTO>(x,
                            await AccountFacade.GetAccountAccordingToIdAsync(
                                (await AuctionFacade.GetAllBidsAccordingToAuction(x.Id))
                                .OrderByDescending(y => y.BidDateTime).First().AccountId));
                }).ToList();
				*/
            AccountDetailModel accountDetailModel = new AccountDetailModel
            {
                AccountDto = account,
				MyAuctions = myAuctions,
				BiddingAuctionsAndLastBid = new List<Pair<AuctionDTO, AccountDTO>>(biddedAuctionsLastBidAccount)
            };
            return View("AccountDeatilView", accountDetailModel);
        }

        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (model.Password == null || model.PasswordAgain == null)
            {
                return View(model);
            }
            if (model.Password == model.PasswordAgain)
            {
                var account = await AccountFacade.GetAccountAccordingToEmailAsync(model.Email);
                account.Password = model.Password;
                var result = await AccountFacade.EditAccountAsync(account);
                return await Login(new LoginModel {EmailAddress = model.Email, Password = model.Password }, null);
            }

            ModelState.AddModelError("", "Entered Passwords aren't same");
            return View(new ChangePasswordModel { Email = model.Email });
		}

        public async Task<ActionResult> ChangeEmail(ChangeEmailModel model)
        {
            if (model.NewEmail == null || model.NewEmailAgain == null)
            {
                return View(model);
            }

            if (model.NewEmail == model.NewEmailAgain)
            {
                var account = await AccountFacade.GetAccountAccordingToEmailAsync(model.Email);
                if ((await AccountFacade.GetAccountAccordingToEmailAsync(model.NewEmail)) != null)
                {
                    ModelState.AddModelError("", "Account with this Email already exist");
                    return View(new ChangeEmailModel { Email = model.Email });
				}
                account.Email = model.NewEmail;
                var result = await AccountFacade.EditAccountAsync(account);
                return await Login(new LoginModel {EmailAddress = model.NewEmail, Password = account.Password}, null);
            }

			ModelState.AddModelError("", "Entered Emails aren't same");
            return View(new ChangeEmailModel {Email = model.Email });
        }
	}
}