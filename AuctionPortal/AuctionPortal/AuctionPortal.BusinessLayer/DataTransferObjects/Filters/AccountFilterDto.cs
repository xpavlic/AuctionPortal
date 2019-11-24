using AuctionPortal.BusinessLayer.DataTransferObjects.Common;

namespace AuctionPortal.BusinessLayer.DataTransferObjects.Filters
{
    public class AccountFilterDto : FilterDtoBase
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdministrator { get; set; }
    }
}
