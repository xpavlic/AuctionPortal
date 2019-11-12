using System.Data.Common;
using System.Data.Entity;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;

namespace AuctionPortal.DataAccessLayer.EntityFramework
{
    public class AuctionPortalDbContext : DbContext
    {
		private const string MartinPC = "Data source=(LocalDB)\\MSSQLLocalDB;AttachDbFileName=C:\\Users\\Martin\\Desktop\\c#\\Database.mdf;Integrated Security = True;";
		private const string School = "Data source=(LocalDB)\\MSSQLLocalDB;AttachDbFileName=J:\\pv179\\Database.mdf;Integrated Security = True;";

		private const string ConnectionString = School;

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

        public DbSet<Account> Accounts { get; set; }

	    public DbSet<Auction> Auctions { get; set; }

	    public DbSet<AccountAuctionRelation> AccountAuctionRelations { get; set; }

	    public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
	}
}
