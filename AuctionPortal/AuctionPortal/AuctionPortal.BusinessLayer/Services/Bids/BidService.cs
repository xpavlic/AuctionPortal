using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.QueryObjects.Common;
using AuctionPortal.BusinessLayer.Services.Common;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure;
using AuctionPortal.Infrastructure.Query;
using AutoMapper;

namespace AuctionPortal.BusinessLayer.Services.Bids
{
    public class BidService : CrudQueryServiceBase<AccountAuctionRelation, AccountAuctionRelationDTO, AccountAuctionRelationFilterDto>, IBidService
    {
        public BidService(IMapper mapper, IRepository<AccountAuctionRelation> repository, QueryObjectBase<AccountAuctionRelationDTO,
            AccountAuctionRelation, AccountAuctionRelationFilterDto, IQuery<AccountAuctionRelation>> query) 
            : base(mapper, repository, query) { }

        protected override Task<AccountAuctionRelation> GetWithIncludesAsync(Guid entityId)
        {
            return Repository.GetAsync(entityId, nameof(AccountAuctionRelation.Account),
                nameof(AccountAuctionRelation.Auction));
        }

        public async Task<IEnumerable<AccountAuctionRelationDTO>> GetAllBidsByAccount(Guid accountId)
        {
            var queryResult = await Query.ExecuteQuery(new AccountAuctionRelationFilterDto { AccountId = accountId});
            return queryResult.Items;
        }

        public async Task<IEnumerable<AccountAuctionRelationDTO>> GetAllBidsByAuction(Guid auctionId)
        {
            var queryResult = await Query.ExecuteQuery(new AccountAuctionRelationFilterDto { AuctionId = auctionId });
            return queryResult.Items;
        }
    }
}
