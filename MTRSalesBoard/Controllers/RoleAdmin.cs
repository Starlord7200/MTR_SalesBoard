using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTRSalesBoard.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleAdminController : Controller
    {
        // Variables
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;

        // Constructor
        public RoleAdminController(RoleManager<IdentityRole> roleMgr,
                                   UserManager<AppUser> userMrg) {
            roleManager = roleMgr;
            userManager = userMrg;
        }

        // Returns role view
        public ViewResult Index() => View(roleManager.Roles.ToList());

        // Returns role creation view
        public IActionResult Create() => View();

        // Creates a role
        [HttpPost]
        public async Task<IActionResult> Create([Required]string name) {
            if (ModelState.IsValid) {
                IdentityResult result
                    = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        // Deletes the role from the DB
        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null) {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    AddErrorsFromResult(result);
                }
            }
            else {
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", roleManager.Roles.ToList());
        }

        // Returns View of current roles and who is in them
        public async Task<IActionResult> Edit(string id) {

            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in userManager.Users.ToList()) {
                var list = await userManager.IsInRoleAsync(user, role.Name)
                    ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        // Edits who is in a role
        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model) {
            IdentityResult result;
            if (ModelState.IsValid) {
                foreach (string userId in model.IdsToAdd ?? new string[] { }) {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null) {
                        result = await userManager.AddToRoleAsync(user,
                            model.RoleName);
                        if (!result.Succeeded) {
                            AddErrorsFromResult(result);
                        }
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { }) {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null) {
                        result = await userManager.RemoveFromRoleAsync(user,
                            model.RoleName);
                        if (!result.Succeeded) {
                            AddErrorsFromResult(result);
                        }
                    }
                }
            }

            return (ModelState.IsValid) ? RedirectToAction(nameof(Index)) : await Edit(model.RoleId);
        }

        // Used to add errors when the model state isn't valid
        private void AddErrorsFromResult(IdentityResult result) {
            foreach (IdentityError error in result.Errors) {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
