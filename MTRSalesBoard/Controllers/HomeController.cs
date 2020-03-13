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
        IRepository Repository;

        public HomeController(IRepository r)
        {
            Repository = r;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SalesEntry() => View();

        [HttpPost]
        public IActionResult SalesEntry(string name, decimal salePrice)
        {
            AppUser user = Repository.UsersList.Find(u2 => u2.Name == name);
            Sale s = new Sale() { SaleAmount = salePrice };
            Repository.AddSale(s);
            user.AddSale(s);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
