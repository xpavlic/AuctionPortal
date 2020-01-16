using AuctionPortal.BusinessLayer.DataTransferObjects;

namespace AuctionPortal.PresentationLayer.Models.AdminAuctions
{
    public class AdminAuctionEditModel
    {
        public AuctionDTO Auction { get; set; }
        public CategoryDTO Category { get; set; }

    }
}