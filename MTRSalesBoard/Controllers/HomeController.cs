using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MTRSalesBoard.Models;
using MTRSalesBoard.Models.Repository;

namespace MTRSalesBoard.Controllers
{
    public class HomeController : Controller
    {
        //TODO: Allow Users to edit sales
        //TODO: Allow Users to delete sales
        //TODO: Allow Sign in
        //TODO: Allow Admin to add/delete sales for people
        //TODO: Allow Admin to add people

        IRepository Repository;
        public HomeController(IRepository r) {
            Repository = r;
        }

        public IActionResult Index() {
            List<AppUser> users = Repository.Users;
            return View(users);
        }

        [HttpGet]
        public IActionResult SalesEntry() => View();

        [HttpPost]
        public IActionResult SalesEntry(string name, decimal salePrice) {
            AppUser user = Repository.FindAppUserbyName(name);

            if (user == null)
            {
                return View("SalesEntry");
            }
            else
            {
                Sale s = new Sale() { SaleAmount = salePrice, SaleDate = DateTime.Today };
                Repository.AddSale(s, user);
            }
            List<AppUser> users = Repository.Users;
            return View("Index", users);
        }

        [HttpGet]
        public IActionResult ViewSales() => View();

        [HttpPost]
        public IActionResult ViewSales(string name) {
            AppUser user = Repository.FindAppUserbyName(name);
            if (user == null)
            {
                return ViewSales();
            }
            else
            {
                return View("ViewSalesList", user);
            }
        }

        public IActionResult UpdateSale(int id) {
            Sale sale = Repository.FindSaleById(id);
            return View();
        }


        public IActionResult ViewSalesList(AppUser u) => View(u);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
