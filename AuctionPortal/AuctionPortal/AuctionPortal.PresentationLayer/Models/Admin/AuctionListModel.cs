using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuctionPortal.BusinessLayer.DataTransferObjects;

namespace AuctionPortal.PresentationLayer.Models.Admin
{
	public class AuctionListModel
	{
		public IList<AuctionDTO> Auctions { get; set; }
		public IList<CategoryDTO> Categories { get; set; }
	}
}