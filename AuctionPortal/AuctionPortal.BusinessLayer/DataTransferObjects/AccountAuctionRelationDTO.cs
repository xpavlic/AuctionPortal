using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    class AccountAuctionRelationDTO : DtoBase
    {
        public Guid AccountId { get; set; }

        public Guid AuctionId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AccountAuctionRelationDTO dTO &&
                   AccountId.Equals(dTO.AccountId) &&
                   AuctionId.Equals(dTO.AuctionId);
        }

        public override int GetHashCode()
        {
            var hashCode = -803818392;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(AccountId);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(AuctionId);
            return hashCode;
        }
    }
}
