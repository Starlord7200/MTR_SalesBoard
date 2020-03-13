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

        List<Sale> Sales { get { return Sales; } }     
        
        public void AddSale(Sale s)
        {
            sales.Add(s);
        }

        public decimal CalcSales()
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
