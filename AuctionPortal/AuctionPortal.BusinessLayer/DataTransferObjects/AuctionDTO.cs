using System;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    public class AuctionDTO
    {
        public string Name { get; set; }

        public decimal ActualPrice { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ProductId { get; set; }
    }
}
