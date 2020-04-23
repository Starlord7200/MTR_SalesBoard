using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class RoleModificationModel
    {
        // This model is used for keeping track of what people to add and remove from a role
        #region Properties
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
        #endregion
    }
}
