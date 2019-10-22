using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Entities
{
	public class Product : IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid Id { get; set; }

		[NotMapped]
		public string TableName { get; } = nameof(AuctionPortalDbContext.Products);

		[Required, MaxLength(256)]
		public string Name { get; set; }

		public string ProductImgUrl { get; set; }

        [ForeignKey(nameof(Auction))]
        public Guid AuctionId { get; set; }

        public virtual Auction Auction { get; set; }
    }
}
