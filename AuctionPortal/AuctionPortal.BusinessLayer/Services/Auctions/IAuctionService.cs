using System;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;

namespace AuctionPortal.BusinessLayer.Services.Auctions
{
    public interface IAuctionService
    {
        Task<QueryResultDto<AuctionDTO, AuctionFilterDto>> ListAuctionsAsync(AuctionFilterDto filter);

        Task<AuctionDTO> GetAsync(Guid entityId, bool withIncludes = true);

        Task<AuctionDTO> GetAuctionByNameAsync(string name);

        Guid Create(AuctionDTO entityDto);

        Task Update(AuctionDTO entityDto);

        void Delete(Guid entityId);

        Task<QueryResultDto<AuctionDTO, AuctionFilterDto>> ListAllAsync();
    }
}
