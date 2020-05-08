using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTRSalesBoard.Models;
using System.Threading.Tasks;

namespace MTRSalesBoard.Controllers
{
    public class AccountController : Controller
    {
        // Private variables
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        // Constructor
        public AccountController(UserManager<AppUser> userMgr,
        SignInManager<AppUser> signinMgr, RoleManager<IdentityRole> roleMgr) {
            userManager = userMgr;
            signInManager = signinMgr;
            roleManager = roleMgr;
        }

        // Returns the Login View
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl) {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        // Handles the login request from the login page. 
        // If it succeeds, it shows the board page depending on which role
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl) {
            if (ModelState.IsValid) {
                IdentityRole role = await roleManager.FindByNameAsync("User");
                AppUser user = await userManager.FindByNameAsync(model.Username);
                if (user != null) {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded) {
                        if (await userManager.IsInRoleAsync(user, role.Name))
                            return Redirect(returnUrl ?? "/");
                        else
                            return RedirectToAction("Board", "Admin");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.Username), "Invalid username or password");
            }
            return View(model);
        }

        // Returns the view page for signup
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp(string returnUrl) {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        // Post Request that handles signing up a user
        // Viewmodel has required traits that check validation amd return errors if the model state isn't valid
        // Redirects to url they were trying to access 
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(RegisterUserViewModel model, string returnUrl) {
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
                    await userManager.AddToRoleAsync(user, "User");
                    return Redirect(returnUrl ?? "/");
                }
                else {
                    foreach (IdentityError error in result.Errors) {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        // Handles Logout action when logout is clicked. Redirects to the login page if succeeded
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }

        // Used for when someone tries to access a controller but doesn't have the role access to get past authorization
        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}
