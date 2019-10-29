using AuctionPortal.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Entities
{
	public class Category : IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid Id { get; set; }

		[NotMapped] 
		public string TableName { get; } = nameof(AuctionPortalDbContext.Categories);

		[Required]
		[MaxLength(256)]
		public string Name { get; set; }

		[NotMapped]
		public bool HasParent => this.Parent != null;

		[ForeignKey(nameof(Parent))]
		public Guid? ParentId { get; set; }

		public virtual Category Parent { get; set; }
	}
}
