using System;
using Xunit;
using MTRSalesBoard.Models;
using MTRSalesBoard.Controllers;
using MTRSalesBoard.Models.Repository;
using System.Collections.Generic;

namespace MTRSalesBoardTests
{
    public class UserSaleTests
    {
        [Fact]
        public void AddUserTest() {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo, null, null);

            //Act
            AppUser user = new AppUser() { Name = "James", Email = "example@example.com" };
            Sale s = new Sale() { SaleAmount = 1000 };

            repo.AddUser(user);
            user.AddSale(s);

            //Assert
            Assert.Equal(1, repo.GetUserCount());
            Assert.Equal("James", repo.Users[repo.Users.Count - 1].Name);
            Assert.Equal("example@example.com", repo.Users[repo.Users.Count - 1].Email);
        }

        [Fact]
        public void CalcTotalUserSalesTest() {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo, null, null);

            //Act
            AppUser user = new AppUser() { Name = "James", Email = "example@example.com" };
            Sale s = new Sale() { SaleAmount = 1000, SaleDate = DateTime.Today };
            Sale s2 = new Sale() { SaleAmount = 3000, SaleDate = DateTime.Today };

            repo.AddUser(user);
            user.AddSale(s);
            user.AddSale(s2);

            //Assert
            Assert.Equal(1, repo.GetUserCount());
            Assert.Equal(2, user.GetSalesCount());
            Assert.Equal(3000, user.Sales[user.Sales.Count - 1].SaleAmount);
            Assert.Equal("James", repo.Users[repo.Users.Count - 1].Name);
            Assert.Equal("example@example.com", repo.Users[repo.Users.Count - 1].Email);
            Assert.Equal(4000, user.CalcTotalUserSales());
        }

        [Fact]
        public void UserSaleListSortTest() {
            //Arrange 
            var repo = new FakeRepository();

            //Act
            AppUser user = new AppUser() { Name = "James", Email = "example@example.com" };
            AppUser user2 = new AppUser() { Name = "Matt", Email = "m@example.com" };
            AppUser user3 = new AppUser() { Name = "Me", Email = "mee@example.com" };
            Sale s = new Sale() { SaleAmount = 10000, SaleDate = DateTime.Now.AddMonths(-1) };
            Sale s2 = new Sale() { SaleAmount = 2888, SaleDate = DateTime.Now.AddMonths(-1) };
            Sale s3 = new Sale() { SaleAmount = 2000, SaleDate = DateTime.Now.AddMonths(-1) };

            repo.AddUser(user3);
            repo.AddUser(user);
            repo.AddUser(user2);

            repo.AddSale(s);
            user.AddSale(s);

            repo.AddSale(s2);
            user2.AddSale(s2);

            repo.AddSale(s3);
            user3.AddSale(s3);

            List<AppUser> users = repo.Users;

            //Assert
            Assert.Equal(3, repo.GetUserCount());
            Assert.Equal("Matt", repo.Users[repo.Users.Count - 1].Name);
            Assert.Equal("m@example.com", repo.Users[repo.Users.Count - 1].Email);
            Assert.Equal(2888, user2.CalcTotalUserSales());

            Assert.Equal("Me", users[0].Name);
            Assert.Equal("James", users[1].Name);
            Assert.Equal("Matt", users[2].Name);


            users.Sort((s1, s2) => decimal.Compare(s1.CalcLastMonthUserSales(), s2.CalcLastMonthUserSales()));
            users.Reverse();

            Assert.Equal("James", users[0].Name);
            Assert.Equal("Matt", users[1].Name);
            Assert.Equal("Me", users[2].Name);

            Sale s4 = new Sale() { SaleAmount = 10000, SaleDate = DateTime.Now.AddMonths(-1) };
            user3.AddSale(s4);

            List<AppUser> usersAgain = repo.Users;
            Assert.Equal("James", users[0].Name);
            Assert.Equal("Matt", users[1].Name);
            Assert.Equal("Me", users[2].Name);

            usersAgain.Sort((s1, s2) => decimal.Compare(s1.CalcLastMonthUserSales(), s2.CalcLastMonthUserSales()));
            usersAgain.Reverse();

            Assert.Equal("Me", users[0].Name);
            Assert.Equal("James", users[1].Name);
            Assert.Equal("Matt", users[2].Name);
        }
    }
}
