using Microsoft.AspNetCore.Identity;
using MTRSalesBoard.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Infrastructure
{
    public class UserListGeneration
    {
        public async Task<List<AppUser>> GenerateAppUserList(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            List<AppUser> users = new List<AppUser>();
            IdentityRole role = await roleManager.FindByNameAsync("User");
            if (role != null)
            {
                foreach (var user in userManager.Users.ToList())
                {
                    if (user != null
                        && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        users.Add(user);
                    }
                }
            }

            return users;
        }
    }

    public class SortingClass
    {
        public static List<AppUser> ListSorter(List<AppUser> users, string title = "")
        {
            switch (title)
            {
                case "Today":
                    return SortByToday(users);
                case "cWeek":
                    return SortByCurrentWeek(users);
                case "lWeek":
                    return SortByLastWeek(users);
                case "2Week":
                    return SortByLastTwoWeeks(users);         
                case "3Week":
                    return SortByLastThreeWeeks(users);                    
                case "4Week":
                    return SortByLastFourWeeks(users);                    
                case "Month":
                    return SortByMonthToDate(users);                    
                case "YTD":
                    return SortByYearToDate(users);                    
                default:
                    return SortByMonthToDate(users);
            }
        }

        //Sorts list by who has the highest sales for the current day
        public static List<AppUser> SortByToday(List<AppUser> users) {
            users.Sort((s1, s2) => decimal.Compare(s1.CalcTodaySalesAmt(), s2.CalcTodaySalesAmt()));
            users.Reverse();
            return users;
        }

        //Sorts list by who has the highest sales for the current week
        public static List<AppUser> SortByCurrentWeek(List<AppUser> users) {
            users.Sort((s1, s2) => decimal.Compare(s1.CalcCurrentWeekSalesAmt(), s2.CalcCurrentWeekSalesAmt()));
            users.Reverse();
            return users;
        }

        //Sorts list by who has the highest sales for the lastest week
        public static List<AppUser> SortByLastWeek(List<AppUser> users) {
            users.Sort((s1, s2) => decimal.Compare(s1.CalcLastWeekUserSales(), s2.CalcLastWeekUserSales()));
            users.Reverse();
            return users;
        }

        //Sorts list by who has the highest sales two weeks ago
        public static List<AppUser> SortByLastTwoWeeks(List<AppUser> users) {
            users.Sort((s1, s2) => decimal.Compare(s1.CalcLastTwoWeekUserSales(), s2.CalcLastTwoWeekUserSales()));
            users.Reverse();
            return users;
        }

        //Sorts list by who has the highest sales three weeks ago
        public static List<AppUser> SortByLastThreeWeeks(List<AppUser> users) {
            users.Sort((s1, s2) => decimal.Compare(s1.CalcLastThreeWeekUserSales(), s2.CalcLastThreeWeekUserSales()));
            users.Reverse();
            return users;
        }

        //Sorts list by who has the highest sales four weeks ago
        public static List<AppUser> SortByLastFourWeeks(List<AppUser> users) {
            users.Sort((s1, s2) => decimal.Compare(s1.CalcLastFourWeekUserSales(), s2.CalcLastFourWeekUserSales()));
            users.Reverse();
            return users;
        }

        //Sorts list by who has the highest sales for the current month
        public static List<AppUser> SortByMonthToDate(List<AppUser> users) {
            users.Sort((s1, s2) => decimal.Compare(s1.CalcMonthToDateUserSales(), s2.CalcMonthToDateUserSales()));
            users.Reverse();
            return users;
        }

        //Sorts list by who has the highest sales last month
        public static List<AppUser> SortByLastMonthToDate(List<AppUser> users) {
            users.Sort((s1, s2) => decimal.Compare(s1.CalcLastMonthUserSales(), s2.CalcLastMonthUserSales()));
            users.Reverse();
            return users;
        }

        //Sorts list by who has the highest sales for the current year
        public static List<AppUser> SortByYearToDate(List<AppUser> users) {
            users.Sort((s1, s2) => decimal.Compare(s1.CalcYearToDateUserSales(), s2.CalcYearToDateUserSales()));
            users.Reverse();
            return users;
        }

        //Sorts list by who has the highest sales
        public static List<AppUser> SortByTotal(List<AppUser> users) {
            users.Sort((s1, s2) => decimal.Compare(s1.CalcTotalUserSales(), s2.CalcTotalUserSales()));
            users.Reverse();
            return users;
        }
    }
}
