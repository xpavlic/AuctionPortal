using System;
using System.Collections.Generic;
using System.Linq;
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
		        List<Account> accs = new List<Account>(context.Accounts);
		        foreach (var VARIABLE in accs)
		        {
			        Console.WriteLine(VARIABLE.ToString());
		        }

				Category parent = new Category
				{
					Id = Guid.Parse("835b8aee-6883-4a4c-9e75-950fc90c3f03"),
					Name = "Parent",
					Parent = null,
					ParentId = null
				};

				Category child = new Category
				{
					Id = Guid.Parse("d527ffa7-0b6c-48f2-8e42-90c6b18f7b81"),
					Name = "Child",
					Parent = parent,
					ParentId = parent.Id
				};

				context.Categories.Add(parent);
				context.Categories.Add(child);
				context.SaveChanges();
			}


			Console.ReadKey();
        }
    }
}
