using System.ComponentModel.DataAnnotations;

namespace AuctionPortal.PresentationLayer.Models.Accounts
{
	public class LoginModel
	{
		[Required]
		public string EmailAddress { get; set; }

		[Required]
		public string Password { get; set; }
	}
}