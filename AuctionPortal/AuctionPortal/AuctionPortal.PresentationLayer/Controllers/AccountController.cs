using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.Facades;
using AuctionPortal.BusinessLayer.Services.Accounts;
using DemoEshop.PresentationLayer.Models.Accounts;

namespace AuctionPortal.PresentationLayer.Controllers
{
	public class AccountController : Controller
	{
		public AccountFacade AccountFacade { get; set; }

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
	}
}