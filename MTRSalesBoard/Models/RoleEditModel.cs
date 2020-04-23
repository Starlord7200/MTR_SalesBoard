using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MTRSalesBoard.Models
{
    public class RoleEditModel
    {
        // This model is used for editing a users role
        #region Properties
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
        #endregion
    }
}
