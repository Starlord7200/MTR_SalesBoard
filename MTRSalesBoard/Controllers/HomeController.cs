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
        //TODO: Allow Admin to add people
        //TODO: Allow Admin to edit people and roles
        ///TODO: Make sales board only list people in the user role

        IRepository Repository;
        private UserManager<AppUser> userManager;
        private Task<AppUser> CurrentUser =>
            userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public HomeController(IRepository r, UserManager<AppUser> userMgr) {
            Repository = r;
            userManager = userMgr;
        }


        public IActionResult Index() {
            List<AppUser> users = userManager.Users.ToList();
            List<Sale> sales = Repository.Sales;

            users.OrderBy(user => user.Name);
            return View(users);
        }

        public IActionResult ViewSalesList(AppUser u) => View(u);

        [HttpGet]
        public IActionResult SalesEntry() => View();

        [HttpPost]
        public async Task<IActionResult> SalesEntry(string name, decimal salePrice) {
            AppUser user = await userManager.FindByNameAsync(name);

            if (user == null)
            {
                return View("SalesEntry");
            }
            else
            {
                Sale s = new Sale() { SaleAmount = salePrice, SaleDate = DateTime.Today };
                Repository.AddSale(s, user);
            }
            List<AppUser> users = userManager.Users.ToList();
            return View("Index", users);
        }

        [HttpGet]
        public IActionResult ViewSales() => View();

        [HttpPost]
        public async Task<IActionResult> ViewSales(string name) {
            AppUser user = await CurrentUser;
            var salesFromDb = Repository.Sales;
            if (user == null)
            {
                return ViewSales();
            }
            else
            {
                return View("ViewSalesList", user);
            }
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
