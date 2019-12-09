using System.Collections.Generic;
using System.Web.Mvc;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using X.PagedList;

namespace AuctionPortal.PresentationLayer.Models.Auctions
{
	public class AuctionListViewModel
	{
		public string[] AuctionSortCriteria => new[]
			{nameof(AuctionDTO.Name), nameof(AuctionDTO.ActualPrice), nameof(AuctionDTO.ClosingTime)};

		public IList<CategoryDTO> Categories { get; set; }

		public IPagedList<AuctionDTO> Auctions { get; set; }

		public AuctionFilterDto Filter { get; set; }

		public SelectList AllSortCriteria => new SelectList(AuctionSortCriteria);
	}
}