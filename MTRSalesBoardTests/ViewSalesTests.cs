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
        public async void ViewSalesTest() {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo, null, null);

            //Act
            AppUser user = new AppUser() { Name = "James", Email = "example@example.com" };
            SaleEntryViewModel s = new SaleEntryViewModel() { SaleAmount = 2000 };

            await controller.SalesEntry(s);
            repo.AddUser(user);


            //Assert

        }
    }
}
