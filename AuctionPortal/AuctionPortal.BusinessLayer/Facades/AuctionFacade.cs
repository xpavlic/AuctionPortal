using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.Facades.Common;
using AuctionPortal.BusinessLayer.Services.Auctions;
using AuctionPortal.Infrastructure.UnitOfWork;

namespace AuctionPortal.BusinessLayer.Facades
{
    class AuctionFacade : FacadeBase
    {
        private readonly IAuctionService auctionService;

        public AuctionFacade(IUnitOfWorkProvider unitOfWorkProvider, IAuctionService auctionService) : base(unitOfWorkProvider)
        {
            this.auctionService = auctionService;
        }
    }
}
