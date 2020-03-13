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
                Email = "James@example.com",
            };
            Repository.AddUser(u);

            AppUser u2 = new AppUser()
            {
                Name = "test",
                Email = "@example.com",
            };
            Repository.AddUser(u2);

            Sale s = new Sale()
            {
                SaleAmount = 1000,
                saleDate = DateTime.Today
            };
            u.AddSale(s);
            Repository.AddSale(s);

            Sale s2 = new Sale()
            {
                SaleAmount = 3000,
                saleDate = DateTime.Today
            };
            u2.AddSale(s2);
            Repository.AddSale(s2);
        }
    }
}
