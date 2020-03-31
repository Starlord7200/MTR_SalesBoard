using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models.Repository
{
    public class Repository : IRepository
    {
        private ApplicationDBContext context;

        public Repository(ApplicationDBContext appDbContext) {
            context = appDbContext;
        }

        public List<AppUser> Users
        {
            get
            {
                return context.Users.Include("Sales")
                                    .ToList();
            }
        }

        public List<Sale> Sales
        {
            get
            {
                return context.Sales
                                    .ToList();
            }
        }

        public void AddUser(AppUser u) {
            context.Users.Add(u);
            context.SaveChanges();
        }

        public AppUser FindAppUserbyName(string Name) {
            AppUser u = Users.Find(u2 => u2.Name == Name);
            return u;
        }

        public void AddSale(Sale s, AppUser User) {
            context.Sales.Add(s);
            context.SaveChanges();

            User.Sales.Add(s);
            context.Update(User);
            context.SaveChanges();
        }

        public int EditSale(Sale s) {
            context.Update(s);
            return context.SaveChanges();
        }

        public int GetUserCount() {
            return context.Users.Count();
        }

        public int GetSalesCount() {
            return context.Sales.Count();
        }

        public decimal CalcTotalSales() {
            decimal amt = 0m;
            foreach (Sale s in Sales)
            {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public Sale FindSaleById(int id) {
            Sale s = Sales.First(s1 => s1.SaleID == id);
            return s;
        }
    }
}
