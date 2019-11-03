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

namespace AuctionPortal.BusinessLayer.Services.Products
{
    public class ProductService : CrudQueryServiceBase<Product, ProductDTO, ProductFilterDto>, IProductService
    {
        public ProductService(IMapper mapper, QueryObjectBase<ProductDTO, Product, ProductFilterDto, IQuery<Product>> productQuery, IRepository<Product> productRepository)
            : base(mapper, productRepository, productQuery) { }

        protected override Task<Product> GetWithIncludesAsync(Guid entityId)
        {
            return Repository.GetAsync(entityId, nameof(Product.Auction));
        }

        public async Task<ProductDTO> GetProductByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new ProductFilterDto { Name = name });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<QueryResultDto<ProductDTO, ProductFilterDto>> ListProductsAsync(ProductFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
