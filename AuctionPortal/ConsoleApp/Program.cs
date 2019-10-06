using System;
using System.Collections.Generic;
using AuctionPortal.DataAccessLayer.EntityFramework;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
	        using (var context = new AuctionPortalDbContext())
	        {
				List<Product> products = new List<Product>(context.Products);
				foreach (var VARIABLE in products)
				{
					Console.WriteLine(VARIABLE.Name);
				}
			}

	        Console.ReadKey();
        }
    }
}
