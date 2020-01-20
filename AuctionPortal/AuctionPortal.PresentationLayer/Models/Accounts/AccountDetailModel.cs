using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using Castle.Core;

namespace AuctionPortal.PresentationLayer.Models.Accounts
{
    public class AccountDetailModel
    {
        public string Email { get; set; }
        public AccountDTO AccountDto { get; set; }

        public List<AuctionDTO> MyAuctions { get; set; }

        public List<Pair<AuctionDTO, AccountDTO>> BiddingAuctionsAndLastBid { get; set; }

        public string FullName => AccountDto.FirstName + " " + AccountDto.LastName;
    }
}