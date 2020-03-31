using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models
{
    public class AppUser
    {
        private List<Sale> sales = new List<Sale>();

        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

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
                                                s.SaleDate.Year == today.Year))
            {
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
            foreach (Sale s in sales.Where(s => s.SaleDate.Day > sundayOfLastWeek.Day &&
                                                s.SaleDate.Day < saturdayOfLastWeek.Day &&
                                                s.SaleDate.Month == sundayOfLastWeek.Month &&
                                                s.SaleDate.Month == saturdayOfLastWeek.Month))
            {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcLastTwoWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfLastWeek = DateTime.Now.AddDays(-14 + offSetAmtSunday);
            var saturdayOfLastWeek = DateTime.Now.AddDays(-14 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.Day > sundayOfLastWeek.Day &&
                                                s.SaleDate.Day < saturdayOfLastWeek.Day &&
                                                s.SaleDate.Month == sundayOfLastWeek.Month &&
                                                s.SaleDate.Month == saturdayOfLastWeek.Month))
            {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcLastThreeWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfLastWeek = DateTime.Now.AddDays(-21 + offSetAmtSunday);
            var saturdayOfLastWeek = DateTime.Now.AddDays(-21 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.Day > sundayOfLastWeek.Day &&
                                                s.SaleDate.Day < saturdayOfLastWeek.Day &&
                                                s.SaleDate.Month == sundayOfLastWeek.Month &&
                                                s.SaleDate.Month == saturdayOfLastWeek.Month))
            {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcLastFourWeekUserSales() {
            DayOfWeek desiredSaturDay = DayOfWeek.Saturday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredSaturDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            var sundayOfLastWeek = DateTime.Now.AddDays(-28 + offSetAmtSunday);
            var saturdayOfLastWeek = DateTime.Now.AddDays(-28 + offSetAmtFriday);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.Day > sundayOfLastWeek.Day &&
                                                s.SaleDate.Day < saturdayOfLastWeek.Day &&
                                                s.SaleDate.Month == sundayOfLastWeek.Month &&
                                                s.SaleDate.Month == saturdayOfLastWeek.Month))
            {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcMonthToDateUserSales() {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.Month == startDate.Month &&
                                           s.SaleDate.Year <= endDate.Year))
            {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcYearToDateUserSales() {
            DateTime now = DateTime.Now;
            var currentYear = now.Year;
            decimal amt = 0m;
            foreach (Sale s in sales.Where(s => s.SaleDate.Year == currentYear))
            {
                amt += s.SaleAmount;
            }
            return amt;
        }

        public decimal CalcTotalUserSales() {
            decimal amt = 0m;
            foreach (Sale s in sales)
            {
                amt += s.SaleAmount;
            }

            return amt;
        }
    }
}
