using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using System;

namespace AuctionPortal.BusinessLayer.DataTransferObjects.Filters
{
    public class AuctionFilterDto : FilterDtoBase
    {
        public Guid[] CategoryIds { get; set; }

        public string[] CategoryNames { get; set; }

        public Guid ProductId { get; set; }

        public DateTime ClosingTime { get; set; }

        public decimal ActualPrice { get; set; }
    }
}
