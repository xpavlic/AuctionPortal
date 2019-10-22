using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Entities
{
	public class Account : IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid Id { get; set; }

		[NotMapped]
		public string TableName { get; } = nameof(AuctionPortalDbContext.Accounts);

        public bool IsAdministrator { get; set; }

		[MaxLength(64)]
		public string FirstName { get; set; }

		[MaxLength(64)]
		public string LastName { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		[Phone]
		public string MobilePhoneNumber { get; set; }

		[MaxLength(1024)]
		public string Address { get; set; }

		public DateTime BirthDate { get; set; }
	}
}
