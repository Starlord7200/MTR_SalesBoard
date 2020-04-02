using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //TODO: Allow Admin to add/delete sales for people
        //TODO: Test for SQL Exception

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
            if (role != null)
            {
                foreach (var user in userManager.Users)
                {
                    if (user != null
                        && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        users.Add(user);
                    }
                }
            }

            return View(users);
        }

        public IActionResult ViewSalesList(AppUser u) => View(u);

        [HttpGet]
        public IActionResult SalesEntry() => View();

        [HttpPost]
        public async Task<RedirectToActionResult> SalesEntry(decimal salePrice) {
            AppUser user = await CurrentUser;
            Sale s = new Sale() { SaleAmount = salePrice, SaleDate = DateTime.Today };
            Repository.AddSale(s, user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewSales() {
            //TODO: Fix
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
        public RedirectToActionResult UpdateSale(string name, int saleid, DateTime date, decimal saleamount) {
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
