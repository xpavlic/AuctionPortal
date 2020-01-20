using AuctionPortal.BusinessLayer.DataTransferObjects.Common;

namespace AuctionPortal.BusinessLayer.DataTransferObjects.Filters
{
    public class CategoryFilterDto : FilterDtoBase
    {
        public string[] Names { get; set; }
    }
}
