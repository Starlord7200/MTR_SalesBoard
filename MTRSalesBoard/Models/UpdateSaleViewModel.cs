using System;
using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class UpdateSaleViewModel
    {
        // This viewmodel maintains the validation from the sales entry form
        //If input is more than 1 or 10,000 it'll respond with an error
        #region Properties
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Amount Required")]
        [Range(1, 10000, ErrorMessage = "Amount must be greater than 1")]
        [RegularExpression("^[0-9]{1,10}", ErrorMessage = "Amount must be a number")]
        public decimal SaleAmount { get; set; }

        public string ModalID { get; set; }
        #endregion
    }
}
