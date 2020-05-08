using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MTRSalesBoard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTRSalesBoard.Infrastructure
{
    // Model only used in helping the index view for RoleAdmin
    // Returns the Users in a role and not in a role. If edit is clicked, it displays the users in the role and not in the role

    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public RoleUsersTagHelper(UserManager<AppUser> usermgr,
                                  RoleManager<IdentityRole> rolemgr) {
            userManager = usermgr;
            roleManager = rolemgr;
        }

        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context,
                TagHelperOutput output) {

            List<string> names = new List<string>();
            IdentityRole role = await roleManager.FindByIdAsync(Role);
            if (role != null) {
                foreach (var user in userManager.Users) {
                    if (user != null
                        && await userManager.IsInRoleAsync(user, role.Name)) {
                        names.Add(user.Name);
                    }
                }
            }

            output.Content.SetContent(names.Count == 0 ?
                "No Users" : string.Join(", ", names));
        }
    }
}
