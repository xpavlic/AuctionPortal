using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;

namespace AuctionPortal.BusinessLayer.Services.Bids
{
    public interface IBidService
    {
        Task<AccountAuctionRelationDTO> GetAsync(Guid entityId, bool withIncludes = true);

        Guid Create(AccountAuctionRelationDTO entityDto);

        Task Update(AccountAuctionRelationDTO entityDto);

        void Delete(Guid entityId);

        Task<QueryResultDto<AccountAuctionRelationDTO, AccountAuctionRelationFilterDto>> ListAllAsync();

        Task<IEnumerable<AccountAuctionRelationDTO>> GetAllBidsByAccount(Guid accountId);

        Task<IEnumerable<AccountAuctionRelationDTO>> GetAllBidsByAuction(Guid auctionId);
    }
}
