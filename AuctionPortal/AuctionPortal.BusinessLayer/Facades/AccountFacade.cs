﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.BusinessLayer.Facades.Common;
using AuctionPortal.BusinessLayer.Services.Accounts;
using AuctionPortal.BusinessLayer.Services.Bids;
using AuctionPortal.Infrastructure.UnitOfWork;

namespace AuctionPortal.BusinessLayer.Facades
{
    public class AccountFacade : FacadeBase
    {
        private readonly IAccountService accountService;
        private readonly IBidService bidService;

        public AccountFacade(IUnitOfWorkProvider unitOfWorkProvider, IAccountService accountService, IBidService bidService) : base(unitOfWorkProvider)
        {
            this.accountService = accountService;
            this.bidService = bidService;
        }

        public async Task<AccountDTO> GetAccountAccordingToIdAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await accountService.GetAccountAccordingToIdAsync(id);
            }
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

        public async Task<bool> EditAccountAsync(AccountDTO accountDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await accountService.GetAsync(accountDto.Id, false) == null)
                {
                    return false;
                }

                await accountService.Update(accountDto);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteAccountAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await accountService.GetAsync(id, false) == null)
                {
                    return false;
                }

                accountService.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        public async Task<IEnumerable<AccountAuctionRelationDTO>> GetAllBidsAccordingToAccount(Guid accountId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await bidService.GetAllBidsByAccount(accountId);
            }
        }
    }
}