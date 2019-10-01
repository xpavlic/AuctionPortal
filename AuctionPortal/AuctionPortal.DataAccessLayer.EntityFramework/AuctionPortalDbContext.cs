using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionPortal.DataAccessLayer.EntityFramework
{
    public class AuctionPortalDbContext : DbContext
    {
        private const string ConnectionString = "Data source=(localdb)\\mssqllocaldb;Database=AuctionPortalDbContext;Trusted_Connection=True;MultipleActiveResultSets=true";

        /// <summary>
        /// Non-parametric ctor used by data access layer
        /// </summary>
        public AuctionPortalDbContext() : base(ConnectionString)
        {
            //Database.SetInitializer(new DatabaseInitializer());
        }

        /// <summary>
        /// Ctor with db connection, required by data access layer tests
        /// </summary>
        /// <param name="connection">The database connection</param>
        public AuctionPortalDbContext(DbConnection connection) : base(connection, true)
        {
            Database.CreateIfNotExists();
        }


        //TODO: sets
    }
}
