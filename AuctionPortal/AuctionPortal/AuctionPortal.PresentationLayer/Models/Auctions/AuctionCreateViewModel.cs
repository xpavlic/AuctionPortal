using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuctionPortal.PresentationLayer.Models.Auctions
{
    public class AuctionCreateViewModel
    {
        public string Name { get; set; }

        public decimal ActualPrice { get; set; }

        public bool IsOpened { get; set; }

        [DataType(DataType.Date)]
        public DateTime ClosingTime { get; set; }

        public string Description { get; set; }

        public string AccountEmail { get; set; }

        public string CategoryName { get; set; }
    }
}