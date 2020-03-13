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
