using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public string CategoryId { get; set; }

        public List<SelectListItem> CategoriesSelectList { get; set; }
    }
}