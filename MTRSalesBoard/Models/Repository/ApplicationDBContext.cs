using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MTRSalesBoard.Models.Repository
{
    public class ApplicationDBContext : IdentityDbContext
    {
        // Constructor
        public ApplicationDBContext(
            DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Sale> Sales { get; set; }

        // Creates the admin account from Appsettings.json
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration) {
            UserManager<AppUser> userManager =
                serviceProvider.GetRequiredService<UserManager<AppUser>>();

            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Getting user info out of Appsettings.json   
            string username = configuration["Data:AdminUser:UserName"];
            string name = configuration["Data:AdminUser:Name"];
            string email = configuration["Data:AdminUser:Email"];
            string password = configuration["Data:AdminUser:Password"];
            string role = configuration["Data:AdminUser:Role"];
            string uRole = configuration["Date:AdminUser:Role2"];

            if (await userManager.FindByNameAsync(username) == null) {
                if (await roleManager.FindByNameAsync(role) == null) {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    if (await roleManager.FindByNameAsync(uRole) == null) {
                        await roleManager.CreateAsync(new IdentityRole(uRole));
                    }
                    AppUser user = new AppUser
                    {
                        Name = name,
                        UserName = username,
                        Email = email
                    };
                    IdentityResult result = await userManager
                    .CreateAsync(user, password);
                    if (result.Succeeded) {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }
    }
}
