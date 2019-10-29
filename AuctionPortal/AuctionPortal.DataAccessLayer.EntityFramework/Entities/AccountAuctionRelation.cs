using AuctionPortal.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Entities
{
	public class AccountAuctionRelation : IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid Id { get; set; }

		[NotMapped]
		public string TableName { get; } = nameof(AuctionPortalDbContext.AccountAuctionRelations);

		[ForeignKey(nameof(Account))]
		public Guid AccountId { get; set; }

		public virtual Account Account { get; set; }

		[ForeignKey(nameof(Auction))]
		public Guid AuctionId { get; set; }

		public virtual Auction Auction { get; set; }
	}
}
