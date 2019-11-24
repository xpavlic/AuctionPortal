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

        public async Task<Guid> RegisterAccountAsync(AccountCreateDTO accountDto)
        {
            var account = Mapper.Map<Account>(accountDto);

            if (await GetIfAccountExistsAsync(account.Email))
            {
                throw new ArgumentException();
            }

            Repository.Create(account);
            return account.Id;
        }

        private async Task<bool> GetIfAccountExistsAsync(string email)
        {
            var queryResult = await Query.ExecuteQuery(new AccountFilterDto { Email = email});
            return (queryResult.Items.Count() == 1);
        }

        public async Task<(bool success, bool isAdministrator)> AuthorizeAccountAsync(string email, string password)
        {
            var accountResult = await Query.ExecuteQuery(new AccountFilterDto { Email = email });
            var account = accountResult.Items.SingleOrDefault();

            var succ = account != null && account.Password == password;
            return (succ, account != null && account.IsAdministrator);
        }


    }
}
