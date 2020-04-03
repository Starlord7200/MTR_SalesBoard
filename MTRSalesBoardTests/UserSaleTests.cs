using System;
using Xunit;
using MTRSalesBoard.Models;
using MTRSalesBoard.Controllers;
using MTRSalesBoard.Models.Repository;

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
        public async void AddUserSaleControllerTest() {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo, null, null);

            //Act
            AppUser user = new AppUser() { Name = "James", Email = "example@example.com" };
            SaleEntryViewModel s = new SaleEntryViewModel() { SaleAmount = 2000 };
            repo.AddUser(user);

            await controller.SalesEntry(s);

            //Assert
            Assert.Equal(1, repo.GetUserCount());
            Assert.Equal("James", repo.Users[repo.Users.Count - 1].Name);
            Assert.Equal("example@example.com", repo.Users[repo.Users.Count - 1].Email);
            Assert.Equal(2000, user.CalcTotalUserSales());
            Assert.Equal(1, repo.GetSalesCount());
        }
    }
}
