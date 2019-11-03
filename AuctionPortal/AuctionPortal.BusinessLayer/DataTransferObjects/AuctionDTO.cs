using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    public class AuctionDTO : DtoBase
    {
        public string Name { get; set; }

        public decimal ActualPrice { get; set; }

        public bool IsOpened { get; set; }

        public DateTime ClosingTime { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ProductId { get; set; }

        public override string ToString()
        {
            string state = IsOpened ? "Yes" : "No";
            return $"Auction {Name} is opened: {state}";
        }

        public override bool Equals(object obj)
        {
            return obj is AuctionDTO dTO &&
                   Name == dTO.Name &&
                   ActualPrice == dTO.ActualPrice &&
                   IsOpened == dTO.IsOpened &&
                   ClosingTime == dTO.ClosingTime &&
                   Description == dTO.Description &&
                   CategoryId.Equals(dTO.CategoryId) &&
                   ProductId.Equals(dTO.ProductId);
        }

        public override int GetHashCode()
        {
            var hashCode = 909073441;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + ActualPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + IsOpened.GetHashCode();
            hashCode = hashCode * -1521134295 + ClosingTime.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(CategoryId);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(ProductId);
            return hashCode;
        }
    }
}
