using System;
using System.Linq;
using MTRSalesBoard.Models.Repository;

namespace MTRSalesBoard.Models
{
    public class SeedData
    {
        // Test data for development

        #region SeedMethod
        public static void Seed(ApplicationDBContext context) {
            if (!context.Users.Any()) {
                {
                    AppUser u = new AppUser()
                    {
                        UserName = "JmanMTR",
                        Name = "James",
                        Email = "james@example.com"
                    };
                    context.Users.Add(u);

                    AppUser u2 = new AppUser()
                    {
                        UserName = "testMTR",
                        Name = "test",
                        Email = "test@example.com"
                    };
                    context.Users.Add(u2);

                    Sale s = new Sale()
                    {
                        SaleAmount = 1000m,
                        SaleDate = DateTime.Parse("03/13/2020")
                    };
                    u.AddSale(s);
                    context.Sales.Add(s);

                    Sale s2 = new Sale()
                    {
                        SaleAmount = 3000m,
                        SaleDate = DateTime.Parse("03/13/2020")
                    };
                    u2.AddSale(s2);
                    context.Sales.Add(s2);

                    Sale s3 = new Sale()
                    {
                        SaleAmount = 1000m,
                        SaleDate = DateTime.Parse("03/16/2020")
                    };
                    u.AddSale(s3);
                    context.Sales.Add(s3);

                    Sale s4 = new Sale()
                    {
                        SaleAmount = 500m,
                        SaleDate = DateTime.Parse("03/20/2020")
                    };
                    u2.AddSale(s4);
                    context.Sales.Add(s4);

                    Sale s5 = new Sale()
                    {
                        SaleAmount = 100m,
                        SaleDate = DateTime.Parse("03/4/2020")
                    };
                    u.AddSale(s5);
                    context.Sales.Add(s5);

                    Sale s6 = new Sale()
                    {
                        SaleAmount = 200m,
                        SaleDate = DateTime.Parse("03/5/2020")
                    };
                    u2.AddSale(s6);
                    context.Sales.Add(s6);

                    Sale s7 = new Sale()
                    {
                        SaleAmount = 50m,
                        SaleDate = DateTime.Parse("02/24/2020")
                    };
                    u.AddSale(s7);
                    context.Sales.Add(s7);

                    Sale s8 = new Sale()
                    {
                        SaleAmount = 50m,
                        SaleDate = DateTime.Parse("02/27/2020")
                    };
                    u2.AddSale(s8);
                    context.Sales.Add(s8);

                    Sale s9 = new Sale()
                    {
                        SaleAmount = 15.13m,
                        SaleDate = DateTime.Now
                    };
                    u.AddSale(s9);
                    context.Sales.Add(s9);

                    context.SaveChanges();
                }
            }

        }
        #endregion
    }
}
