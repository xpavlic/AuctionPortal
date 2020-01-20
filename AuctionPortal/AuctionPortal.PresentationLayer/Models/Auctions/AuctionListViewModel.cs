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

        public IPagedList<AuctionDTO> Auctions { get; set; }

		public AuctionFilterDto Filter { get; set; }

		public SelectList AllSortCriteria => new SelectList(AuctionSortCriteria);

        public List<SelectListItem> CategoriesSelectList { get; set; }

		public string CategoryId { get; set; }
	}
}