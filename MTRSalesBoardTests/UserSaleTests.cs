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
        public void AddUserTest()
        {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo);

            //Act
            AppUser user = new AppUser() { Name = "James", Email = "example@example.com" };
            Sale s = new Sale() { SaleAmount = 1000 };

            repo.AddUser(user);
            user.AddSale(s);

            //Assert
            Assert.Equal(1, repo.GetUserCount());
            Assert.Equal("James", repo.UsersList[repo.UsersList.Count - 1].Name);
            Assert.Equal("example@example.com", repo.UsersList[repo.UsersList.Count - 1].Email);
        }

        [Fact]
        public void CalcTotalUserSalesTest()
        {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo);

            //Act
            AppUser user = new AppUser() { Name = "James", Email = "example@example.com" };
            Sale s = new Sale() { SaleAmount = 1000 };
            Sale s2 = new Sale() { SaleAmount = 3000 };

            repo.AddUser(user);
            user.AddSale(s);
            user.AddSale(s2);

            //Assert
            Assert.Equal(1, repo.GetUserCount());
            Assert.Equal(2, user.GetSalesCount());
            Assert.Equal(3000, user.Sales[user.Sales.Count - 1].SaleAmount);
            Assert.Equal("James", repo.UsersList[repo.UsersList.Count - 1].Name);
            Assert.Equal("example@example.com", repo.UsersList[repo.UsersList.Count - 1].Email);
            Assert.Equal(4000, user.CalcUserSales());
        }

        [Fact]
        public void AddUserSaleControllerTest()
        {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo);

            //Act
            AppUser user = new AppUser() { Name = "James", Email = "example@example.com" };           
            repo.AddUser(user);

            controller.SalesEntry("James", 2000);

            //Assert
            Assert.Equal(1, repo.GetUserCount());
            Assert.Equal("James", repo.UsersList[repo.UsersList.Count - 1].Name);
            Assert.Equal("example@example.com", repo.UsersList[repo.UsersList.Count - 1].Email);
            Assert.Equal(2000, user.CalcUserSales());
            Assert.Equal(1, repo.GetSalesCount());
        }
    }
}
