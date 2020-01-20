using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuctionPortal.BusinessLayer.DataTransferObjects;

namespace AuctionPortal.PresentationLayer.Models.Admin
{
	public class AccountEditModel
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public string MobilePhoneNumber { get; set; }

		public string Address { get; set; }

		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }

		public IList<AuctionDTO> Auctions { get; set; }

		public IList<AccountAuctionRelationDTO> Bids { get; set; } 

		public Guid AccountId { get; set; }
    }
}