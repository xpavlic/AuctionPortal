using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.Facades.Common;
using AuctionPortal.BusinessLayer.Services.Accounts;
using AuctionPortal.BusinessLayer.Services.Auctions;
using AuctionPortal.BusinessLayer.Services.Bids;
using AuctionPortal.BusinessLayer.Services.Categories;
using AuctionPortal.BusinessLayer.Services.Products;
using AuctionPortal.Infrastructure.UnitOfWork;

namespace AuctionPortal.BusinessLayer.Facades
{
    public class AuctionFacade : FacadeBase
    {
        private readonly IAuctionService auctionService;
        private readonly ICategoryService categoryService;
        private readonly IAccountService accountService;
        private readonly IBidService bidService;

        public AuctionFacade(IUnitOfWorkProvider unitOfWorkProvider, IAuctionService auctionService, ICategoryService categoryService, 
            IProductService productService, IAccountService accountService, IBidService bidService) 
            : base(unitOfWorkProvider)
        {
            this.auctionService = auctionService;
            this.categoryService = categoryService;
            this.accountService = accountService;
            this.bidService = bidService;
        }

        public async Task<QueryResultDto<AuctionDTO, AuctionFilterDto>> GetAllAuctionsAsync(AuctionFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
				if (filter.CategoryIds == null && filter.CategoryNames != null)
				{
					filter.CategoryIds = await categoryService.GetCategoryIdsByNamesAsync(filter.CategoryNames);
				}
				return await auctionService.ListAuctionsAsync(filter);
			}
        }

        public async Task<AuctionDTO> GetAuctionAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await auctionService.GetAsync(id);
            }
        }

        public async Task<AuctionDTO> GetAuctionAsync(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await auctionService.GetAuctionByNameAsync(name);
            }
        }

        public async Task<Guid> CreateAuctionWithCategoryNameAsync(AuctionDTO auction, string categoryName)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                auction.CategoryId = (await categoryService.GetCategoryIdsByNamesAsync(categoryName)).FirstOrDefault();
                var auctionId = auctionService.Create(auction);
                await uow.Commit();
                return auctionId;
            }
        }

        public async Task<bool> EditAuctionAsync(AuctionDTO auctionDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await auctionService.GetAsync(auctionDto.Id, false) == null)
                {
                    return false;
                }

                await auctionService.Update(auctionDto);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteAuctionAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await auctionService.GetAsync(id, false) == null)
                {
                    return false;
                }

                auctionService.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        public async Task<CategoryDTO> GetCategoryAsync(Guid categoryId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await categoryService.GetAsync(categoryId);
            }
        }

        public async Task<Guid[]> GetProductCategoryIdsByNamesAsync(params string[] names)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await categoryService.GetCategoryIdsByNamesAsync(names);
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await categoryService.ListAllAsync()).Items;
            }
        }

        public async Task<bool> BidOnAuctionAsync(Guid auctionId, Guid accountId, decimal bidValue)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var auctionDto = await auctionService.GetAsync(auctionId);
                var accountDto = await accountService.GetAsync(accountId);

                if (auctionDto == null || accountDto == null || !auctionDto.IsOpened)
                {
                    return false;
                }

                bidService.Create(new AccountAuctionRelationDTO
                {
                    AuctionId = auctionId,
                    AccountId = accountId,
                    BidValue = bidValue
                });

                await uow.Commit();
                return true;
            }
        }
    }
}
