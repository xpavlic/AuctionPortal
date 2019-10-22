using System;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    public class ProductDTO
    {
        public string Name { get; set; }

        public string ProductImgUrl { get; set; }

        public Guid AuctionId { get; set; }
    }
}
