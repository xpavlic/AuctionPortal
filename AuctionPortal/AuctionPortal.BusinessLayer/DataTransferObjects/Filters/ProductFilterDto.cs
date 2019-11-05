using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using System;

namespace AuctionPortal.BusinessLayer.DataTransferObjects.Filters
{
    public class ProductFilterDto : FilterDtoBase
    {
        public string Name { get; set; }
    }
}
