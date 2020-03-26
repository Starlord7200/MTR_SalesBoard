using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MTRSalesBoard.Models;
using Microsoft.Extensions.DependencyInjection;
using MTRSalesBoard.Models.Repository;

namespace MTRSalesBoard.Models
{
    public class SeedData
    {
        public static void Seed(ApplicationDBContext context)
        {
            if (!context.Users.Any())
            {
                {
                    AppUser u = new AppUser()
                    {
                        Name = "James",
                        Email = "james@example.com"
                    };
                    context.Users.Add(u);

                    AppUser u2 = new AppUser()
                    {
                        Name = "test",
                        Email = "test@example.com"
                    };
                    context.Users.Add(u2);

                    Sale s = new Sale()
                    {
                        SaleAmount = 1000,
                        saleDate = DateTime.Parse("03/13/2020")
                    };
                    u.AddSale(s);
                    context.Sales.Add(s);

                    Sale s2 = new Sale()
                    {
                        SaleAmount = 3000,
                        saleDate = DateTime.Parse("03/13/2020")
                    };
                    u2.AddSale(s2);
                    context.Sales.Add(s2);

                    Sale s3 = new Sale()
                    {
                        SaleAmount = 1000,
                        saleDate = DateTime.Parse("03/16/2020")
                    };
                    u.AddSale(s3);
                    context.Sales.Add(s3);

                    Sale s4 = new Sale()
                    {
                        SaleAmount = 500,
                        saleDate = DateTime.Parse("03/20/2020")
                    };
                    u2.AddSale(s4);
                    context.Sales.Add(s4);

                    Sale s5 = new Sale()
                    {
                        SaleAmount = 100,
                        saleDate = DateTime.Parse("03/4/2020")
                    };
                    u.AddSale(s5);
                    context.Sales.Add(s5);

                    Sale s6 = new Sale()
                    {
                        SaleAmount = 200,
                        saleDate = DateTime.Parse("03/5/2020")
                    };
                    u2.AddSale(s6);
                    context.Sales.Add(s6);

                    Sale s7 = new Sale()
                    {
                        SaleAmount = 50,
                        saleDate = DateTime.Parse("02/24/2020")
                    };
                    u.AddSale(s7);
                    context.Sales.Add(s7);

                    Sale s8 = new Sale()
                    {
                        SaleAmount = 50,
                        saleDate = DateTime.Parse("02/27/2020")
                    };
                    u2.AddSale(s8);
                    context.Sales.Add(s8);

                    Sale s9 = new Sale()
                    {
                        SaleAmount = 15,
                        saleDate = DateTime.Now
                    };
                    u.AddSale(s9);
                    context.Sales.Add(s9);
                }
            }
 
        }
    }
}
