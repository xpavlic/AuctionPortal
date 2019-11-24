using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using System;

namespace AuctionPortal.BusinessLayer.DataTransferObjects.Filters
{
    public class AccountAuctionRelationFilterDto : FilterDtoBase
    {
        public Guid AccountId { get; set; }

        public Guid AuctionId { get; set; }
    }
}
