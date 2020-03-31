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
        //TODO: Allow Sign in
        //TODO: Allow Admin to add/delete sales for people
        //TODO: Allow Admin to add people
        //TODO: Allow Admin to edit people and roles

        IRepository Repository;
        public HomeController(IRepository r) {
            Repository = r;
        }

        public IActionResult Index() {
            List<AppUser> users = Repository.Users;
            return View(users);
        }

        public IActionResult ViewSalesList(AppUser u) => View(u);

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
