using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionPortal.PresentationLayer.Models.Accounts
{
    public class ChangePasswordModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordAgain { get; set; }
    }
}