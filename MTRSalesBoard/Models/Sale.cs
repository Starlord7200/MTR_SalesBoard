using System;
using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class Sale
    {
        [Key]
        public int SaleID { get; set; }
        public decimal SaleAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public AppUser Name { get; set; }
    }
}
