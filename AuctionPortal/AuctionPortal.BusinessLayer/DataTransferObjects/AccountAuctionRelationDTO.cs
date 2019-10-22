using System;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    class AccountAuctionRelationDTO
    {
        public Guid AccountId { get; set; }

        public Guid AuctionId { get; set; }
    }
}
