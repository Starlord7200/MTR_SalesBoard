using System;
using System.Collections.Generic;
using System.Linq;

namespace MTRSalesBoard.Models.Repository
{
    public class Repository : IRepository
    {
        // Repository used for the application. 
        // Manages the updating and saving of the database

        #region Properties
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
        #endregion

        #region Find Methods
        // Finds a sale in the DB with matching ID
        public Sale FindSaleById(int id) {
            Sale s = Sales.First(s1 => s1.SaleID == id);
            return s;
        }
        #endregion

        #region Update/Edit/Add Methods
        // Adds a user to the DB
        public void AddUser(AppUser u) {
            context.Users.Add(u);
            context.SaveChanges();
            context.Dispose();
        }

        // Adds a sale to the DB
        // Adds sale to user sale list
        public void AddSale(Sale s, AppUser User) {
            this.context.Sales.Add(s);
            this.context.SaveChanges();

            User.Sales.Add(s);
            this.context.Update(User);
            this.context.SaveChanges();
            this.context.Dispose();
        }

        // Updates the sale in the DB
        public int EditSale(Sale s) {
            context.Update(s);
            return context.SaveChanges();
        }

        // Deletes a user from the DB
        // Removes reference to all sales pertaining to them before deletion
        public void DeleteUser(AppUser u) {
            if (u.Sales.Count > 0) {
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
            context.SaveChanges();
        }

        // Deletes a sale from the DB
        public void DeleteSale(int id) {
            var saleFromDb = context.Sales.First(s1 => s1.SaleID == id);
            context.Remove(saleFromDb);
            context.SaveChanges();
            context.Dispose();
        }
        #endregion

        #region Calulation Methods
        // Returns the count of every user in the DB
        public int GetUserCount() {
            return context.Users.Count();
        }

        // Returns the count of all sales in the DB
        public int GetSalesCount() {
            return context.Sales.Count();

        }

        // Returns the total amount of all sales made
        public decimal CalcTotalSales() {
            decimal amt = 0m;
            foreach (Sale s in Sales.ToList()) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        // Returns the amount from all sales made for the current month
        public decimal CalcMonthYearSales(decimal month, decimal year) {
            decimal amt = 0m;
            foreach (Sale s in Sales.Where(s => s.SaleDate.Month == month &&
                                            s.SaleDate.Year == year)) {
                amt += s.SaleAmount;
            }

            return amt;
        }
        // Returns the amount for all sales made last year
        public decimal CalcLastYearSales() {
            var year = DateTime.Now.AddYears(-1);
            decimal amt = 0m;
            foreach (Sale s in Sales.Where(s => s.SaleDate.Year == year.Year)) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        // Returns the amount from all sales made for the current month last year
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
        #endregion
    }
}
