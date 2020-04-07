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

        IRepository Repository;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;

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

        public ViewResult Index() => View(userManager.Users);

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model) {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.UserName,
                    Name = model.Name,
                    Email = model.Email
                };
                IdentityResult result
                    = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("Index", userManager.Users);
        }

        public async Task<IActionResult> Edit(string id) {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email,
                string password, string name, string username) {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UserName = username;
                user.Name = name;
                user.Email = email;
                IdentityResult validUserName
                    = await userValidator.ValidateAsync(userManager, user);
                if (!validUserName.Succeeded)
                {
                    AddErrorsFromResult(validUserName);
                }
                IdentityResult validEmail
                    = await userValidator.ValidateAsync(userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager,
                        user, password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user,
                            password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null)
                        || (validEmail.Succeeded
                        && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EnterSaleUser() {
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

        [HttpGet]
        public IActionResult EnterSale(string title) {
            ViewBag.user = title;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnterSale(string name, SaleEntryViewModel model) {
            AppUser user = await userManager.FindByNameAsync(name);

            if (user == null)
            {
                return RedirectToAction("EnterSaleUser");
            }
            else
            {
                Sale s = new Sale() { SaleAmount = model.SaleAmount, SaleDate = DateTime.Today };
                Repository.AddSale(s, user);
            }
            return RedirectToAction("Board");
        }

        public async Task<IActionResult> Board() {
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

        [HttpGet]
        public async Task<IActionResult> ViewUserSale() {
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

        public async Task<IActionResult> ViewSalesList(string title) {
            AppUser u = await userManager.FindByNameAsync(title);
            List<Sale> s = Repository.Sales;
            return View(u);
        }

        private void AddErrorsFromResult(IdentityResult result) {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
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
            return RedirectToAction("Board");
        }

        public RedirectToActionResult DeleteSale(int id) {
            Repository.DeleteSale(id);
            return RedirectToAction("Board");
        }

    }
}
