using System;
using System.Collections.Generic;

namespace AuctionPortal.BusinessLayer.DataTransferObjects.Common
{
    public class QueryResultDto<TDto, TFilter> where TFilter : FilterDtoBase
    {
        public long TotalItemsCount { get; set; }

        public int? RequestedPageNumber { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<TDto> Items { get; set; }

        public TFilter Filter { get; set; }

        public override string ToString()
        {
            return $"{TotalItemsCount} {typeof(TDto).Name}(s)" +
                   $"{(RequestedPageNumber != null ? $", page {RequestedPageNumber}/{Math.Ceiling(TotalItemsCount / (double)PageSize)}." : ".")}";
        }
    }
}
