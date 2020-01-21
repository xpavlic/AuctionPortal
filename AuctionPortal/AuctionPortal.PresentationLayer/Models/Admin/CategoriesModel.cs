using System.Collections.Generic;
using AuctionPortal.BusinessLayer.DataTransferObjects;

namespace AuctionPortal.PresentationLayer.Models.Admin
{
	public class CategoriesModel
	{
		public IList<CategoryDTO> Categories { get; set; }

		public string NewCategoryName { get; set; }
	}
}