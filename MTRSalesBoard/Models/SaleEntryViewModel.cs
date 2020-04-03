using System;
using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class SaleEntryViewModel
    {
        [Required(ErrorMessage = "Sale amount is required must be greater than $1")]
        [Range(1, 999999999)]
        public decimal SaleAmount { get; set; }
    }
}
