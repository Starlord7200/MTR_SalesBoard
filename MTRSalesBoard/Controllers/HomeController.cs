using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MTRSalesBoard.Models;
using MTRSalesBoard.Models.Repository;

namespace MTRSalesBoard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<AppUser> users = Repository.UsersList;
            return View(users);
        }

        [HttpGet]
        public IActionResult SalesEntry() => View();

        [HttpPost]
        public IActionResult SalesEntry(string name, string email, decimal salePrice)
        {
            AppUser user = Repository.FindAppUserbyName(name);
            
            if(user == null)
            {
                AppUser u = new AppUser() { Name = name, Email = email };
                Sale sa = new Sale() { SaleAmount = salePrice, saleDate = DateTime.Today };
                Repository.AddUser(u);
                Repository.AddSale(sa);
                u.AddSale(sa);
            }
            else
            {
                Sale s = new Sale() { SaleAmount = salePrice, saleDate = DateTime.Today };
                Repository.AddSale(s);
                user.AddSale(s);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ViewSales() => View();

        [HttpPost]
        public IActionResult ViewSales(string name)
        {
            AppUser user = Repository.FindAppUserbyName(name);
            return View("ViewSalesList",user);
        }
        public IActionResult ViewSalesList(AppUser u) => View(u);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
