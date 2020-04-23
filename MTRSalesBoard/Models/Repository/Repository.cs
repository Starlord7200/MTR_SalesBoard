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

        public void AddSale(Sale s, AppUser User) {
            this.context.Sales.Add(s);
            this.context.SaveChanges();

            User.Sales.Add(s);
            this.context.Update(User);
            this.context.SaveChanges();
        }

        public int EditSale(Sale s) {
            context.Update(s);
            return context.SaveChanges();
        }

        public void DeleteUser(AppUser u) {
            var salesFromDb = context.Sales;
            foreach (Sale s in u.Sales) {
                u.Sales.Remove(s);
                context.Update(u);
                context.SaveChanges();

                var saleFromDb = context.Sales.First(s1 => s1.SaleID == s.SaleID);
                saleFromDb.Name = null;
                context.Update(saleFromDb);
                context.SaveChanges();
            }
        }

        public void DeleteSale(int id) {
            var saleFromDb = context.Sales.First(s1 => s1.SaleID == id);
            context.Remove(saleFromDb);
            context.SaveChanges();
        }

        public int GetUserCount() {
            return context.Users.Count();
        }

        public int GetSalesCount() {
            return context.Sales.Count();
        }

        public decimal CalcTotalSales() {
            decimal amt = 0m;
            foreach (Sale s in Sales) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcMonthYearSales(decimal month, decimal year) {
            decimal amt = 0m;
            foreach (Sale s in Sales.Where(s => s.SaleDate.Month == month &&
                                            s.SaleDate.Year == year)) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcMonthLastYearSales() {
            decimal amt = 0m;
            var month = DateTime.Now.Month;
            var lastYear = DateTime.Now.AddYears(-1);
            foreach (Sale s in Sales.Where(s => s.SaleDate.Month == month &&
                                            s.SaleDate.Year == lastYear.Year)) {
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
