using System;
using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class Sale
    {
        // This model is the model for sales
        // Requires an application user to be attached
        #region Properties
        [Key]
        public int SaleID { get; set; }
        public decimal SaleAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public AppUser Name { get; set; }
        #endregion
    }
}
