using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MTRSalesBoard.Models
{
    public class AppUser : IdentityUser
    {
        private List<Sale> sales = new List<Sale>();

        public string Name { get; set; }

        public List<Sale> Sales { get { return sales; } }

        public void AddSale(Sale s) {
            Sales.Add(s);
        }

        public int GetSalesCount() {
            return Sales.Count();
        }

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

        public decimal CalcLastWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfLastWeek = DateTime.Now.AddDays(-7 + offSetAmtSunday);
            var saturdayOfLastWeek = DateTime.Now.AddDays(-7 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.DayOfYear > sundayOfLastWeek.DayOfYear &&
                                                s.SaleDate.DayOfYear < saturdayOfLastWeek.DayOfYear &&
                                                s.SaleDate.Year >= sundayOfLastWeek.Year &&
                                                s.SaleDate.Year <= saturdayOfLastWeek.Year &&
                                                s.SaleDate.Month >= sundayOfLastWeek.Month &&
                                                s.SaleDate.Month <= saturdayOfLastWeek.Month)) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcLastTwoWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfWeek = DateTime.Now.AddDays(-14 + offSetAmtSunday);
            var saturdayOfWeek = DateTime.Now.AddDays(-14 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.DayOfYear > sundayOfWeek.DayOfYear &&
                                                s.SaleDate.DayOfYear < saturdayOfWeek.DayOfYear &&
                                                s.SaleDate.Year >= sundayOfWeek.Year &&
                                                s.SaleDate.Year <= saturdayOfWeek.Year &&
                                                s.SaleDate.Month >= sundayOfWeek.Month &&
                                                s.SaleDate.Month <= saturdayOfWeek.Month)) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcLastThreeWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfWeek = DateTime.Now.AddDays(-21 + offSetAmtSunday);
            var saturdayOfWeek = DateTime.Now.AddDays(-21 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.DayOfYear > sundayOfWeek.DayOfYear &&
                                                s.SaleDate.DayOfYear < saturdayOfWeek.DayOfYear &&
                                                s.SaleDate.Year >= sundayOfWeek.Year &&
                                                s.SaleDate.Year <= saturdayOfWeek.Year &&
                                                s.SaleDate.Month >= sundayOfWeek.Month &&
                                                s.SaleDate.Month <= saturdayOfWeek.Month)) {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcLastFourWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfWeek = DateTime.Now.AddDays(-28 + offSetAmtSunday);
            var saturdayOfWeek = DateTime.Now.AddDays(-28 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.DayOfYear > sundayOfWeek.DayOfYear &&
                                                s.SaleDate.DayOfYear < saturdayOfWeek.DayOfYear &&
                                                s.SaleDate.Year >= sundayOfWeek.Year &&
                                                s.SaleDate.Year <= saturdayOfWeek.Year &&
                                                s.SaleDate.Month >= sundayOfWeek.Month &&
                                                s.SaleDate.Month <= saturdayOfWeek.Month)) {
                amt += s.SaleAmount;
            }

            return amt;
        }

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

        public decimal CalcYearToDateUserSales() {
            DateTime now = DateTime.Now;
            var currentYear = now.Year;
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.Year == currentYear)) {
                amt += s.SaleAmount;
            }
            return amt;
        }

        public decimal CalcTotalUserSales() {
            decimal amt = 0m;
            foreach (Sale s in sales) {
                amt += s.SaleAmount;
            }

            return amt;
        }
    }
}
