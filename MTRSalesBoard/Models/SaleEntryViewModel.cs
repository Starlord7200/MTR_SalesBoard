using System;
using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class SaleEntryViewModel
    {
        // This viewmodel maintains the validation from the sales entry form
        //If input is more than 1 or 10,000 it'll respond with an error
        #region Properties
        [Required]
        [Range(1, 10000)]
        public decimal SaleAmount { get; set; }
        #endregion
    }
}
