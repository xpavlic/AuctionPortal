using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.Facades.Common;
using AuctionPortal.BusinessLayer.Services.Accounts;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure.UnitOfWork;
using Microsoft.Win32;

namespace AuctionPortal.BusinessLayer.Facades
{
    public class AccountFacade : FacadeBase
    {
        private readonly IAccountService accountService;

        public AccountFacade(IUnitOfWorkProvider unitOfWorkProvider, IAccountService accountService) : base(unitOfWorkProvider)
        {
            this.accountService = accountService;
        }


        public async Task<AccountDTO> GetAccountAccordingToEmailAsync(string email)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await accountService.GetAccountAccordingToEmailAsync(email);
            }
        }

        public async Task<QueryResultDto<AccountDTO, AccountFilterDto>> GetAllAccountsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await accountService.ListAllAsync();
            }
        }

        public async Task<Guid> RegisterAccount(AccountCreateDTO accountCreateDto) 
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var id = await accountService.RegisterAccountAsync(accountCreateDto);
                    await uow.Commit();
                    return id;
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }
        }

        public async Task<(bool succes, bool isAdministrator)> Login(string email, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await accountService.AuthorizeAccountAsync(email, password);
            }
        }
    }
}
