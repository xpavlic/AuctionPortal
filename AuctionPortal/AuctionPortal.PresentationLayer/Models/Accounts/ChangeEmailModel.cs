using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionPortal.PresentationLayer.Models.Accounts
{
    public class ChangeEmailModel
    {
        public string Email { get; set; }

        public string NewEmail { get; set; }

        public string NewEmailAgain { get; set; }
    }
}