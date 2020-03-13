using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models.Repository
{
    public class Repository
    {
        private List<AppUser> usersList = new List<AppUser>();
        private List<Sale> salesList = new List<Sale>();
        public List<AppUser> UsersList { get { return usersList; } }
        public List<Sale> SalesList { get { return salesList; } }

        public void AddUser(AppUser u)
        {
            UsersList.Add(u);
        }

        public void AddSale(Sale s)
        {
            SalesList.Add(s);
        }

        public int GetUserCount()
        {
            return UsersList.Count();
        }

        public int GetSalesount()
        {
            return UsersList.Count();
        }

    }
}
