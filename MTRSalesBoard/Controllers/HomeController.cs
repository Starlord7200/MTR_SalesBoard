using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MTRSalesBoard.Models;
using MTRSalesBoard.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;

namespace MTRSalesBoard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IRepository Repository;
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private Task<AppUser> CurrentUser =>
            userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public HomeController(IRepository r, UserManager<AppUser> userMgr, RoleManager<IdentityRole> roleMgr) {
            Repository = r;
            userManager = userMgr;
            roleManager = roleMgr;
        }

        public async Task<IActionResult> Index() {
            List<Sale> sales = Repository.Sales;
            List<AppUser> users = new List<AppUser>();

            IdentityRole role = await roleManager.FindByNameAsync("User");
            if (role != null) {
                foreach (var user in userManager.Users) {
                    if (user != null
                        && await userManager.IsInRoleAsync(user, role.Name)) {
                        users.Add(user);
                    }
                }
            }

            users.Sort((s1, s2) => decimal.Compare(s1.CalcLastMonthUserSales(), s2.CalcLastMonthUserSales()));
            users.Reverse();

            return View(users);
        }

        public IActionResult ViewSalesList(AppUser u) => View(u);

        [HttpGet]
        public IActionResult SalesEntry() => View();

        [HttpPost]
        public async Task<IActionResult> SalesEntry(SaleEntryViewModel model) {
            if (ModelState.IsValid) {
                AppUser user = await CurrentUser;
                Sale s = new Sale() { SaleAmount = model.SaleAmount, SaleDate = DateTime.Today, Name = user };
                Repository.AddSale(s, user);
                return RedirectToAction("Index");
            }
            else {
                ModelState.AddModelError("", "Sale Amount Required");
                return View(model);
            }

        }

        public async Task<IActionResult> ViewSales() {
            AppUser user = await CurrentUser;
            var salesFromDb = Repository.Sales;
            return View("ViewSalesList", user);
        }

        [HttpGet]
        public IActionResult UpdateSale(int id) {
            Sale sale = Repository.FindSaleById(id);
            return View(sale);
        }

        [HttpPost]
        public RedirectToActionResult UpdateSale(int saleid, DateTime date, decimal saleamount) {
            Sale s = new Sale
            {
                SaleID = saleid,
                SaleDate = date,
                SaleAmount = saleamount
            };

            Repository.EditSale(s);
            return RedirectToAction("index");
        }

        public RedirectToActionResult DeleteSale(int id) {
            Repository.DeleteSale(id);
            return RedirectToAction("Index");
        }
    }
}
