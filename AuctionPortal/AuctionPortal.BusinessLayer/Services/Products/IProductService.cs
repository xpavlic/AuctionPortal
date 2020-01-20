using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;

namespace AuctionPortal.BusinessLayer.Services.Products
{
    public interface IProductService
    {
        Task<QueryResultDto<ProductDTO, ProductFilterDto>> ListProductsAsync(ProductFilterDto filter);

        Task<ProductDTO> GetAsync(Guid entityId, bool withIncludes = true);

        Task<ProductDTO> GetProductByNameAsync(string name);

        Guid Create(ProductDTO entityDto);

        Task Update(ProductDTO entityDto);

        void Delete(Guid entityId);

        Task<QueryResultDto<ProductDTO, ProductFilterDto>> ListAllAsync();

        Task<IEnumerable<ProductDTO>> GetAllProductAcordingToAuction(Guid auctionId);
    }
}
