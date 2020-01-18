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
                    Console.WriteLine(VARIABLE.Id);
                }

                List<Product> asd = new List<Product>(context.Products);
                foreach (var VARIABLE in asd)
                {
                    Console.WriteLine(VARIABLE.ToString());
                }
                List<Category> cat = new List<Category>(context.Categories);
                foreach (var VARIABLE in cat)
                {
                    Console.WriteLine(VARIABLE.Name);
                }

                Console.ReadKey();

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
                    Name = "NoCategory",
                    Parent = null,
                    ParentId = null
                };

                Auction auction = new Auction
                {
                    Id = Guid.Parse("7ad0b015-5651-441c-9879-44b322d03986"),
                    ActualPrice = 100,
                    Category = child,
                    CategoryId = child.Id,
                    ClosingTime = new DateTime(2020, 1, 1),
                    Description = "asd0",
                    IsOpened = true,
                    Name = "MyAuction"
                };

                Product product = new Product
                {
                    Id = Guid.Parse("0cf65f4d-a568-481d-b73f-fdc026a75cce"),
                    Auction = auction,
                    AuctionId = auction.Id,
                    Name = "MyProduct",
                    ProductImgUrl = "asdsd"
                };

                //context.Categories.Add(parent);
                //context.Categories.Add(child);
                ////context.Auctions.Add(auction);
                ////context.Products.Add(product);
                //context.SaveChanges();
            }
            Console.ReadKey();
        }
    }
}