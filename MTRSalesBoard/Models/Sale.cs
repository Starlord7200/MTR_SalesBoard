using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models
{
    public class Sale
    {
        [Key]
        public int SaleID { get; set; }
        public decimal SaleAmount { get; set; }
        public DateTime saleDate { get; set; } 
        public AppUser name { get; set; }
    }
}
