using System;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;

namespace AuctionPortal.BusinessLayer.DataTransferObjects.Filters
{
    public class AccountFilterDto : FilterDtoBase
    {
        public string Email { get; set; }

        public Guid Id { get; set; }
    }
}