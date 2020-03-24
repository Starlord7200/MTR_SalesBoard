using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models.Repository
{
    public static class Repository
    {
        private static List<AppUser> usersList = new List<AppUser>();
        private static List<Sale> salesList = new List<Sale>();

        static Repository()
        {
            AddTestData();
        }

        public static List<AppUser> UsersList { get { return usersList; } }
        public static List<Sale> SalesList { get { return salesList; } }

        public static void AddUser(AppUser u)
        {
            UsersList.Add(u);
        }

        public static AppUser FindAppUserbyName(string Name)
        {
            AppUser u = UsersList.Find(u2 => u2.Name == Name);
            return u;
        }

        public static void AddSale(Sale s)
        {
            SalesList.Add(s);
        }

        public static int GetUserCount()
        {
            return UsersList.Count();
        }

        public static int GetSalesCount()
        {
            return SalesList.Count();
        }

        public static decimal CalcTotalSales()
        {
            decimal amt = 0;
            foreach (Sale s in SalesList)
            {
                amt += s.SaleAmount;
            }

            return amt;
        }

        static void AddTestData()
        {
            AppUser u = new AppUser()
            {
                Name = "James",
                Email = "james@example.com"
            };
            Repository.AddUser(u);

            AppUser u2 = new AppUser()
            {
                Name = "test",
                Email = "test@example.com"
            };
            Repository.AddUser(u2);

            Sale s = new Sale()
            {
                SaleAmount = 1000,
                saleDate = DateTime.Parse("03/13/2020")
            };
            u.AddSale(s);
            Repository.AddSale(s);

            Sale s2 = new Sale()
            {
                SaleAmount = 3000,
                saleDate = DateTime.Parse("03/13/2020")
            };
            u2.AddSale(s2);
            Repository.AddSale(s2);

            Sale s3 = new Sale()
            {
                SaleAmount = 1000,
                saleDate = DateTime.Parse("03/16/2020")
            };
            u.AddSale(s3);
            Repository.AddSale(s3);

            Sale s4 = new Sale()
            {
                SaleAmount = 500,
                saleDate = DateTime.Parse("03/20/2020")
            };
            u2.AddSale(s4);
            Repository.AddSale(s4);

            Sale s5 = new Sale()
            {
                SaleAmount = 100,
                saleDate = DateTime.Parse("03/4/2020")
            };
            u.AddSale(s5);
            Repository.AddSale(s5);

            Sale s6 = new Sale()
            {
                SaleAmount = 200,
                saleDate = DateTime.Parse("03/5/2020")
            };
            u2.AddSale(s6);
            Repository.AddSale(s6);

            Sale s7 = new Sale()
            {
                SaleAmount = 50,
                saleDate = DateTime.Parse("02/24/2020")
            };
            u.AddSale(s7);
            Repository.AddSale(s7);

            Sale s8 = new Sale()
            {
                SaleAmount = 50,
                saleDate = DateTime.Parse("02/27/2020")
            };
            u2.AddSale(s8);
            Repository.AddSale(s8);

            Sale s9 = new Sale()
            {
                SaleAmount = 10,
                saleDate = DateTime.Now
            };
            u.AddSale(s9);
            Repository.AddSale(s9);
        }
    }
}
