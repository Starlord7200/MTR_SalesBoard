using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MTRSalesBoard.Models
{
    public class AppUser : IdentityUser
    {
        #region Properties
        private List<Sale> sales = new List<Sale>();

        public string Name { get; set; }

        public List<Sale> Sales { get { return sales; } }

        #endregion

        #region Methods
        // Adds a sale to the Sales list for this particular user
        public void AddSale(Sale s) {
            Sales.Add(s);
        }

        // Returns the cound of sales in the Sales List fpr the user
        public int GetSalesCount() {
            return Sales.Count();
        }

        // Returns the totals for all sales pertaining to the user for Today's date
        public decimal CalcTodaySalesAmt() {
            DateTime today = DateTime.Now;
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.Day == today.Day &&
                                                s.SaleDate.Month == today.Month &&
                                                s.SaleDate.Year == today.Year)) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        // Returns the total for all sales pertaining to the user for the Current Week
        public decimal CalcCurrentWeekSalesAmt() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfWeek = DateTime.Now.AddDays(offSetAmtSunday);
            var saturdayOfWeek = DateTime.Now.AddDays(offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => Between(s.SaleDate, sundayOfWeek, saturdayOfWeek))) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        // Returns the total for all sales pertaining to the user last Week
        public decimal CalcLastWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfWeek = DateTime.Now.AddDays(-7 + offSetAmtSunday);
            var saturdayOfWeek = DateTime.Now.AddDays(-7 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => Between(s.SaleDate, sundayOfWeek, saturdayOfWeek))) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        // Returns the total for all sales pertaining to the user Two Weeks ago
        public decimal CalcLastTwoWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfWeek = DateTime.Now.AddDays(-14 + offSetAmtSunday);
            var saturdayOfWeek = DateTime.Now.AddDays(-14 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => Between(s.SaleDate, sundayOfWeek, saturdayOfWeek))) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        // Returns the total for all sales pertaining to the user Three Weeks Ago
        public decimal CalcLastThreeWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfWeek = DateTime.Now.AddDays(-21 + offSetAmtSunday);
            var saturdayOfWeek = DateTime.Now.AddDays(-21 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => Between(s.SaleDate, sundayOfWeek, saturdayOfWeek))) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        // Returns the total for all sales pertaining to the user Four Weeks Ago
        public decimal CalcLastFourWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfWeek = DateTime.Now.AddDays(-28 + offSetAmtSunday);
            var saturdayOfWeek = DateTime.Now.AddDays(-28 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => Between(s.SaleDate, sundayOfWeek, saturdayOfWeek))) {
                amt += s.SaleAmount;
            }
            return amt;
        }

        // Returns the total for all sales pertaining to the user during the current month
        public decimal CalcMonthToDateUserSales() {
            DateTime now = DateTime.Now;
            var month = now.Month;
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.Month == month &&
                                           s.SaleDate.Year == now.Year)) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        // Returns the total for all sales pertaining to the user during the last month
        public decimal CalcLastMonthUserSales() {
            var lastMonth = DateTime.Now.AddMonths(-1);
            var lastMonthYear = lastMonth.Year;
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.Month == lastMonth.Month &&
                                           s.SaleDate.Year == lastMonthYear)) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        // Returns the total for all sales pertaining to the user during the current year
        public decimal CalcYearToDateUserSales() {
            DateTime now = DateTime.Now;
            var currentYear = now.Year;
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.Year == currentYear)) {
                amt += s.SaleAmount;
            }
            return amt;
        }

        // Returns the total for all sales pertaining to the user
        public decimal CalcTotalUserSales() {
            decimal amt = 0m;
            foreach (Sale s in sales) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        private static bool Between(DateTime dateToCheck, DateTime startDate, DateTime endDate) {
            return (dateToCheck > startDate && dateToCheck < endDate);
        }
        #endregion
    }
}
