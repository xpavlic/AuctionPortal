using System;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Entities
{
    public interface IEntity
    {
        /// <summary>
        /// Unique id of the entity.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Name of the database table for this entity.
        /// </summary>
        string TableName { get; }
    }
}
