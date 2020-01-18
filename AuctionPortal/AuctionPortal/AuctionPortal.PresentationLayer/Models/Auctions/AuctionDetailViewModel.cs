using System;
using System.Collections.Generic;
using AuctionPortal.BusinessLayer.DataTransferObjects;

namespace AuctionPortal.PresentationLayer.Models.Auctions
{
    public class AuctionDetailViewModel
    {
        public string Name { get; set; }

        public decimal ActualPrice { get; set; }

        public bool IsOpened { get; set; }

        public DateTime ClosingTime { get; set; }

        public string Description { get; set; }

        public string AccountFullName { get; set; }

        public List<ProductDTO> Products { get; set; }
    }
}