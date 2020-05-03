using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MTRSalesBoard.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using MTRSalesBoard.Models.Repository;
using System.Collections.Generic;
using System.Linq;

namespace MTRSalesBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Variables
        IRepository Repository;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;

        // Constructor
        public AdminController(UserManager<AppUser> usrMgr,
                IUserValidator<AppUser> userValid,
                IPasswordValidator<AppUser> passValid,
                IPasswordHasher<AppUser> passwordHash, IRepository r, RoleManager<IdentityRole> roleMgr) {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            Repository = r;
            roleManager = roleMgr;
        }

        // Returns view page for Admin Index
        public ViewResult Index() => View(userManager.Users);

        // Returns view page for Create Page
        public ViewResult Create() => View();

        // Post request that creates a user. If it succeeds, it returns the associated view
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model) {
            if (ModelState.IsValid) {
                AppUser user = new AppUser
                {
                    UserName = model.UserName,
                    Name = model.Name,
                    Email = model.Email
                };
                IdentityResult result
                    = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    foreach (IdentityError error in result.Errors) {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        // Deletes a user from the db. 
        // Removes the association to all sales
        // If it succeeds, user is deleted
        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            var salesFromDb = Repository.Sales;
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null) {
                try {
                    Repository.DeleteUser(user);
                }
                catch {
                    ModelState.AddModelError("", "Unable to delete user");
                }

                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    AddErrorsFromResult(result);
                }
            }
            else {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("Index", userManager.Users);
        }

        // Returns view page for editing a user
        public async Task<IActionResult> Edit(string id) {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null) {
                return View(user);
            }
            else {
                return RedirectToAction("Index");
            }
        }

        // Handles the edit request of a user, If the user isn't null, validation is checked. If succeeded, user is updated in the database
        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email,
                string password, string name, string username) {
            AppUser user = await userManager.FindByIdAsync(id);
            try {
                if (user != null) {
                    user.UserName = username;
                    user.Name = name;
                    user.Email = email;
                    IdentityResult validUserName
                        = await userValidator.ValidateAsync(userManager, user);
                    if (!validUserName.Succeeded) {
                        AddErrorsFromResult(validUserName);
                    }
                    IdentityResult validEmail
                        = await userValidator.ValidateAsync(userManager, user);
                    if (!validEmail.Succeeded) {
                        AddErrorsFromResult(validEmail);
                    }
                    IdentityResult validPass = null;
                    if (!string.IsNullOrEmpty(password)) {
                        validPass = await passwordValidator.ValidateAsync(userManager,
                            user, password);
                        if (validPass.Succeeded) {
                            user.PasswordHash = passwordHasher.HashPassword(user,
                                password);
                        }
                        else {
                            AddErrorsFromResult(validPass);
                        }
                    }
                    if ((validEmail.Succeeded && validPass == null)
                            || (validEmail.Succeeded
                            && password != string.Empty && validPass.Succeeded)) {
                        IdentityResult result = await userManager.UpdateAsync(user);
                        if (result.Succeeded) {
                            return RedirectToAction("Index");
                        }
                        else {
                            AddErrorsFromResult(result);
                        }
                    }
                }
                else {
                    ModelState.AddModelError("", "User Not Found");
                }
                return View(user);
            }
            catch {
                ModelState.AddModelError("", "unable to edit the user at this time");
                return View(user);
            }
        }

        // Returns a table of users currently in the user role
        [HttpGet]
        public async Task<IActionResult> EnterSaleUser() {
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
            return View(users);
        }

        // Handles the form post request. Checks validation to make sure that the form has a number
        // If validation passes, The sale is created and added to the user
        // Redirects to the sales board if succeeded
        [HttpPost]
        public async Task<IActionResult> EnterSale(string name, SaleEntryViewModel model) {
            AppUser user = await userManager.FindByNameAsync(name);

            if (user == null) {
                return RedirectToAction("EnterSaleUser");
            }
            else if (ModelState.IsValid) {
                Sale s = new Sale() { SaleAmount = model.SaleAmount, SaleDate = DateTime.Today };
                Repository.AddSale(s, user);
            }
            else {
                ViewBag.user = user.UserName;
                return View(model);
            }
            return RedirectToAction("Board");
        }

        // Obtains all sales and users from the DB
        // Finds all users in the User role
        // Sorts users based on the sales total from last months
        // Returns the Adminboard view
        public async Task<IActionResult> Board() {
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

            ViewBag.CurrentMonthAll = Repository.CalcMonthYearSales(DateTime.Now.Month, DateTime.Now.Year).ToString("c");
            ViewBag.LastMonthAll = Repository.CalcMonthYearSales(DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).Year).ToString("c");
            ViewBag.LastYearMonthAll = Repository.CalcMonthLastYearSales().ToString("c");

            return View(users);
        }

        // Retruns a table full of people that have made a sale in the user role
        [HttpGet]
        public async Task<IActionResult> ViewUserSale() {
            List<AppUser> users = new List<AppUser>();

            IdentityRole role = await roleManager.FindByNameAsync("User");
            if (role != null) {
                foreach (var user in userManager.Users.Where(u => u.Sales.Count != 0)) {
                    if (user != null) {
                        users.Add(user);
                    }
                }
            }

            return View(users);
        }

        // Obtains the sales for a specific user
        // Returns View with sales details for the user
        public async Task<IActionResult> ViewSalesList(string title) {
            AppUser u = await userManager.FindByNameAsync(title);
            List<Sale> s = Repository.Sales;
            return View(u);
        }

        // Takes in a sale ID
        // Find sale and returns a form view
        // Returns the view to update a sale
        [HttpGet]
        public IActionResult UpdateSale(int id) {
            Sale sale = Repository.FindSaleById(id);
            UpdateSaleViewModel model = new UpdateSaleViewModel
            {
                Id = sale.SaleID,
                SaleAmount = sale.SaleAmount,
                Date = sale.SaleDate
            };
            return View(model);
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

        // Deletes sale from the database
        // Returns the sales board when it completes
        public RedirectToActionResult DeleteSale(int id) {
            Repository.DeleteSale(id);
            return RedirectToAction("Board");
        }

        // Used for adding error results to page. Private 
        private void AddErrorsFromResult(IdentityResult result) {
            foreach (IdentityError error in result.Errors) {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
