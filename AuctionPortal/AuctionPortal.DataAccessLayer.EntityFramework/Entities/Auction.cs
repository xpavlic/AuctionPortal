using AuctionPortal.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Entities
{
	public class Auction : IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid Id { get; set; }

		[NotMapped]
		public string TableName { get; } = nameof(AuctionPortalDbContext.Auctions);

		[Required, MaxLength(256)]
		public string Name { get; set; }

		[Range(0, int.MaxValue)]
		public decimal ActualPrice { get; set; }

        public bool IsOpened { get; set; }

        public DateTime ClosingTime { get; set; }

		[MaxLength(65536)]
		public string Description { get; set; }

		[ForeignKey(nameof(Category))]
		public Guid CategoryId { get; set; }

		public virtual Category Category { get; set; }

		[ForeignKey(nameof(Account))]
		public Guid AccountId { get; set; }

		public virtual Account Account { get; set; }
	}
}
