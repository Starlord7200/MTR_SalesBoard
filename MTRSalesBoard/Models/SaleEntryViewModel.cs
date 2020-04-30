﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class SaleEntryViewModel
    {
        // This viewmodel maintains the validation from the sales entry form
        //If input is more than 1 or 10,000 it'll respond with an error
        #region Properties
        [Required(ErrorMessage = "Sale Amount Required")]
        [Range(1, 10000, ErrorMessage = "Amount must be greater than 1")]
        public decimal SaleAmount { get; set; }
        #endregion
    }
}
