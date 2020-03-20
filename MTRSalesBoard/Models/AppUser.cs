using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models
{
    public class AppUser
    {
        private List<Sale> sales = new List<Sale>();

        public string Name { get; set; }
        public string Email { get; set; }

        public List<Sale> Sales { get { return sales; } }     
        
        public void AddSale(Sale s)
        {
            Sales.Add(s);
        }

        public int GetSalesCount()
        {
            return Sales.Count();
        }

        public decimal CalcLastWeekUserSales()
        {
            DayOfWeek desiredFriDay = DayOfWeek.Friday;
            DayOfWeek desiredSunDay = DayOfWeek.Sunday;
            int offSetAmtFriday = (int)desiredFriDay - (int)DateTime.Now.DayOfWeek;
            int offSetAmtSunday = (int)desiredSunDay - (int)DateTime.Now.DayOfWeek;
            //var sundayOfLastWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            var sundayOfLastWeek = DateTime.Now.AddDays(-7 + offSetAmtSunday);
            var fridayOfLastWeek = DateTime.Now.AddDays(-7 + offSetAmtFriday);
            decimal amt = 0;
            foreach (Sale s in sales.Where(s => s.saleDate.Day > sundayOfLastWeek.Day || s.saleDate.Day < fridayOfLastWeek.Day))
            {
                amt += s.SaleAmount;
            }

            return amt;
        }

        public decimal CalcUserSales()
        {
            decimal amt = 0; 
            foreach (Sale s in sales)
            {
                amt += s.SaleAmount;
            }

            return amt;
        }
    }
}
