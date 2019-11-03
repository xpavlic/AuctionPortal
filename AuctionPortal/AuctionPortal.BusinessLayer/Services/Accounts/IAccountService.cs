using System;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;

namespace AuctionPortal.BusinessLayer.Services.Accounts
{
    public interface IAccountService
    {
        Task<AccountDTO> GetAccountAccordingToEmailAsync(string email);

        Task<AccountDTO> GetAsync(Guid entityId, bool withIncludes = true);

        Guid Create(AccountDTO entityDto);

        Task Update(AccountDTO entityDto);

        void Delete(Guid entityId);

        Task<QueryResultDto<AccountDTO, AccountFilterDto>> ListAllAsync();
    }
}
