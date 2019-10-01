using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
