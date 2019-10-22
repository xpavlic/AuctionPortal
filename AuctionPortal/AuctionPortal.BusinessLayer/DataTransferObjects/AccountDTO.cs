using System;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    public class AccountDTO
    {
        public bool IsAdministrator { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
