using System;
using Xunit;
using MTRSalesBoard.Models;
using MTRSalesBoard.Controllers;
using MTRSalesBoard.Models.Repository;

namespace MTRSalesBoardTests
{
    public class ViewSalesTests
    {
        [Fact]
        public void ViewSalesTest() {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo, null);

            //Act
            AppUser user = new AppUser() { Name = "James", Email = "example@example.com" };
            Sale s = new Sale() { SaleAmount = 1000 };

            controller.SalesEntry(user.Name, s.SaleAmount);
            repo.AddUser(user);
            user.AddSale(s);

            //Assert

        }
    }
}
