using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTRSalesBoard.Models;
using MTRSalesBoard.Models.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTRSalesBoard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // Variables
        IRepository Repository;
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        // This method gets the current user signed in
        private Task<AppUser> CurrentUser =>
            userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        // Constructor
        public HomeController(IRepository r, UserManager<AppUser> userMgr, RoleManager<IdentityRole> roleMgr) {
            Repository = r;
            userManager = userMgr;
            roleManager = roleMgr;
        }

        // Obtains all sales and users from the DB
        // Finds all users in the User role
        // Sorts users based on the sales total from last months
        // Returns the user board view
        public async Task<IActionResult> Index() {
            List<Sale> sales = Repository.Sales;
            List<AppUser> users = new List<AppUser>();

            IdentityRole role = await roleManager.FindByNameAsync("User");

            if (role != null && users.Count > 0) {
                foreach (var user in userManager.Users) {
                    if (user != null
                        && await userManager.IsInRoleAsync(user, role.Name)) {
                        users.Add(user);
                    }
                }
                users.Sort((s1, s2) => decimal.Compare(s1.CalcLastMonthUserSales(), s2.CalcLastMonthUserSales()));
                users.Reverse();

                return View(users);
            }
            else {
                return View(users);
            }
        }

        // Returns View 
        public IActionResult ViewSalesList(AppUser u) => View(u);

        // Handles Post request
        // Validates sale amount
        // Retruns error if not a number
        // Creates sale and inserts it into the database. Add's reference to current signed user aswell
        [HttpPost]
        public async Task<IActionResult> SalesEntry(SaleEntryViewModel model) {
            if (ModelState.IsValid) {
                AppUser user = await CurrentUser;
                Sale s = new Sale() { SaleAmount = model.SaleAmount, SaleDate = DateTime.Today, Name = user };
                Repository.AddSale(s, user);
                return RedirectToAction("Index");
            }
            else {
                ModelState.AddModelError("", "Something went wrong");
                return RedirectToAction("Index");
            }

        }

        // Returns the sales for a user
        public async Task<IActionResult> ViewSales() {
            AppUser user = await CurrentUser;
            var salesFromDb = Repository.Sales;
            return View("ViewSalesList", user);
        }


        // Handles update post request
        // Updates the sale and add it to the DB
        [HttpPost]
        public IActionResult UpdateSale(UpdateSaleViewModel model) {
            if (ModelState.IsValid) {
                Sale s = new Sale
                {
                    SaleID = model.Id,
                    SaleDate = model.Date,
                    SaleAmount = model.SaleAmount
                };

                Repository.EditSale(s);
                return RedirectToAction("index");
            }
            else {
                return View(model);
            }
        }

        // Deletes sale from the DB
        public RedirectToActionResult DeleteSale(int id) {
            Repository.DeleteSale(id);
            return RedirectToAction("ViewSales");
        }
    }
}
