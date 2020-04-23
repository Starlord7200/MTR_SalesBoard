using System;
using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class SaleEntryViewModel
    {
        [Required]
        [Range(1, 10000)]
        public decimal SaleAmount { get; set; }
    }
}
