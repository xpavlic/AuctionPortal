using System;
using System.Linq;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.QueryObjects.Common;
using AuctionPortal.BusinessLayer.Services.Common;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure;
using AuctionPortal.Infrastructure.Query;
using AutoMapper;

namespace AuctionPortal.BusinessLayer.Services.Auctions
{
    public class AuctionService : CrudQueryServiceBase<Auction, AuctionDTO, AuctionFilterDto>, IAuctionService
    {
        public AuctionService(IMapper mapper, QueryObjectBase<AuctionDTO, Auction, AuctionFilterDto, IQuery<Auction>> productQuery, IRepository<Auction> productRepository)
            : base(mapper, productRepository, productQuery) { }

        protected override Task<Auction> GetWithIncludesAsync(Guid entityId)
        {
            return Repository.GetAsync(entityId, nameof(Auction.Category), nameof(Auction.Product));
        }

        public async Task<AuctionDTO> GetAuctionByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto { Name = name });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<QueryResultDto<AuctionDTO, AuctionFilterDto>> ListAuctionsAsync(AuctionFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
