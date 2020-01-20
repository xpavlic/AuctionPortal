using System;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;

namespace AuctionPortal.DataAccessLayer.EntityFramework
{
    public sealed class AuctionPortalDbContext : DbContext
    {
		//private const string MartinPC = "Data source=(LocalDB)\\MSSQLLocalDB;AttachDbFileName=C:\\Users\\Martin\\Desktop\\c#\\Database.mdf;Integrated Security = True;";
		//private const string School = "Data source=(LocalDB)\\MSSQLLocalDB;AttachDbFileName=J:\\pv179\\Database.mdf;Integrated Security = True;";
		private const string HonzaPC = "Data source=(LocalDB)\\MSSQLLocalDB;AttachDbFileName=C:\\Users\\honza\\OneDrive\\Plocha\\C# Podzim\\Database.mdf;Integrated Security = True;";
        private const string ConnectionString = HonzaPC;

            //$"Data source=(LocalDB)\\MSSQLLocalDB;AttachDbFileName={AppDomain.CurrentDomain.BaseDirectory}..\\Database.mdf;Integrated Security = True;"

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
