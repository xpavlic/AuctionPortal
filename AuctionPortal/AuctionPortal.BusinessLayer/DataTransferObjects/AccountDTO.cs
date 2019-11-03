using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    public class AccountDTO : DtoBase
    {
        public bool IsAdministrator { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public override string ToString() => $"{FirstName} {LastName}'s Account";

        public override bool Equals(object obj)
        {
            return obj is AccountDTO dTO &&
                   IsAdministrator == dTO.IsAdministrator &&
                   FirstName == dTO.FirstName &&
                   LastName == dTO.LastName &&
                   Email == dTO.Email &&
                   MobilePhoneNumber == dTO.MobilePhoneNumber &&
                   Address == dTO.Address &&
                   BirthDate == dTO.BirthDate;
        }

        public override int GetHashCode()
        {
            var hashCode = 1573204586;
            hashCode = hashCode * -1521134295 + IsAdministrator.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MobilePhoneNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + BirthDate.GetHashCode();
            return hashCode;
        }
    }
}
