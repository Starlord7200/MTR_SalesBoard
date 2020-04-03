using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models
{
    public class SaleEntryViewModel
    {
        [Required(ErrorMessage = "Sale amount is required must be greater than $1")]
        [Range(1, 999999999)]
        public decimal SaleAmount { get; set; }
    }
}
