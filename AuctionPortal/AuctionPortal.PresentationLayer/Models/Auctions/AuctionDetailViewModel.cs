using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using Castle.Core;

namespace AuctionPortal.PresentationLayer.Models.Auctions
{
    public class AuctionDetailViewModel
    {
        public string Name { get; set; }

        public Guid Id { get; set; }

        public decimal ActualPrice { get; set; }

        public bool IsOpened { get; set; }

        public string IsOpenedStr => IsOpened ? "yes" : "no";

        [DataType(DataType.Date)]
        public DateTime ClosingTime { get; set; }

        public string Description { get; set; }

        public string AccountFullName { get; set; }

        public string AuctionOwnerEmail { get; set; }

        public List<ProductDTO> Products { get; set; }

        public List<Pair<AccountAuctionRelationDTO, AccountDTO>> Bids { get; set; }

        public decimal NewBidValue { get; set; }

        public string EmailOfBidAccount { get; set; }
        public List<SelectListItem> CategoriesSelectList { get; set; }

        public string CategoryId { get; set; }

    }
}