using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models.Repository
{
    public class FakeRepository : IRepository
    {
        private List<AppUser> usersList = new List<AppUser>();
        private List<Sale> salesList = new List<Sale>();
        public List<AppUser> UsersList { get { return usersList; } }
        public List<Sale> SalesList { get { return salesList; } }

        public void AddUser(AppUser u)
        {
            UsersList.Add(u);
        }

        public AppUser FindAppUserbyName(string Name)
        {
            AppUser u = UsersList.Find(u2 => u2.Name == Name);
            return u;
        }

        public void AddSale(Sale s)
        {
            SalesList.Add(s);
        }

        public int GetUserCount()
        {
            return UsersList.Count();
        }

        public int GetSalesCount()
        {
            return SalesList.Count();
        }

        public decimal CalcTotalSales()
        {
            decimal amt = 0;
            foreach (Sale s in SalesList)
            {
                amt += s.SaleAmount;
            }

            return amt;
        }
    }
}
