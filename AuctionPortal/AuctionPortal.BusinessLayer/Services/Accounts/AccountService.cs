using System;
using System.Linq;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.QueryObjects.Common;
using AuctionPortal.BusinessLayer.Services.Common;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure;
using AuctionPortal.Infrastructure.Query;
using AutoMapper;

namespace AuctionPortal.BusinessLayer.Services.Accounts
{
    public class AccountService : CrudQueryServiceBase<Account, AccountDTO, AccountFilterDto>, IAccountService
    {
        public AccountService(IMapper mapper, IRepository<Account> accountRepository, QueryObjectBase<AccountDTO, Account, AccountFilterDto, IQuery<Account>> accountQueryObject)
            : base(mapper, accountRepository, accountQueryObject) { }

        protected override async Task<Account> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<AccountDTO> GetAccountAccordingToEmailAsync(string email)
        {
            var queryResult = await Query.ExecuteQuery(new AccountFilterDto { Email = email });
            return queryResult.Items.SingleOrDefault();
        }
    }
}
