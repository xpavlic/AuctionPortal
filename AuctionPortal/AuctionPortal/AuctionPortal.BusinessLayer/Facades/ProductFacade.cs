using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.Facades.Common;
using AuctionPortal.BusinessLayer.Services.Products;
using AuctionPortal.Infrastructure.UnitOfWork;

namespace AuctionPortal.BusinessLayer.Facades
{
    public class ProductFacade : FacadeBase
    {
        private readonly IProductService productService;

        public ProductFacade(IUnitOfWorkProvider unitOfWorkProvider, ProductService productService) 
            : base(unitOfWorkProvider)
        {
            this.productService = productService;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsInAuction(Guid auctionId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await productService.GetAllProductAcordingToAuction(auctionId);
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAuctionsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await productService.ListAllAsync()).Items;
            }
        }

        public async Task<ProductDTO> GetProductAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await productService.GetAsync(id);
            }
        }

        public async Task<Guid> CreateProductAsync(ProductDTO productDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var productId = productService.Create(productDto);
                await uow.Commit();
                return productId;
            }
        }

        public async Task<bool> EditProductAsync(ProductDTO productDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await productService.GetAsync(productDto.Id, false) == null)
                {
                    return false;
                }

                await productService.Update(productDto);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await productService.GetAsync(id, false) == null)
                {
                    return false;
                }

                productService.Delete(id);
                await uow.Commit();
                return true;
            }
        }
    }
}
