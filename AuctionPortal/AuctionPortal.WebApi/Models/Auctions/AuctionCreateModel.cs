using System.ComponentModel.DataAnnotations;
using AuctionPortal.BusinessLayer.DataTransferObjects;

namespace AuctionPortal.WebApi.Models.Auctions
{
	public class AuctionCreateModel
	{
		/// <summary>
		/// Product to create.
		/// </summary>
		public AuctionDTO Auction { get; set; }

		/// <summary>
		/// Name of the category to assign auction to.
		/// </summary>
		[Required, MinLength(2), MaxLength(256)]
		public string CategoryName { get; set; }
	}
}