using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure.Query;
using AutoMapper;

namespace AuctionPortal.BusinessLayer.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
			config.CreateMap<Product, ProductDTO>().ReverseMap();
			config.CreateMap<Category, CategoryDTO>().ReverseMap();
			config.CreateMap<Account, AccountDTO>().ReverseMap();
			config.CreateMap<Auction, AuctionDTO>().ReverseMap();
			config.CreateMap<Account, AccountCreateDTO>().ReverseMap();
			config.CreateMap<AccountAuctionRelation, AccountAuctionRelationDTO>().ReverseMap();
			config.CreateMap<QueryResult<Product>, QueryResultDto<ProductDTO, ProductFilterDto>>();
			config.CreateMap<QueryResult<Category>, QueryResultDto<CategoryDTO, CategoryFilterDto>>();
			config.CreateMap<QueryResult<Account>, QueryResultDto<AccountDTO, AccountFilterDto>>();
			config.CreateMap<QueryResult<Auction>, QueryResultDto<AuctionDTO, AuctionFilterDto>>();
			config.CreateMap<QueryResult<AccountAuctionRelation>, QueryResultDto<AccountAuctionRelationDTO, AccountAuctionRelationFilterDto>>();
		}
    }
}
