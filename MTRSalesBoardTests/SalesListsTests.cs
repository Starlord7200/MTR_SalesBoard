using System;
using Xunit;
using MTRSalesBoard.Models;
using MTRSalesBoard.Controllers;
using MTRSalesBoard.Models.Repository;

namespace MTRSalesBoardTests
{
    public class SalesListsTests
    {
        [Fact]
        public void AddSaleTest()
        {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo);

            //Act
            Sale s = new Sale() { SaleAmount = 1000, saleDate = DateTime.Today };
            repo.AddSale(s);

            //Assert
            Assert.Equal(1, repo.GetSalesCount());
            Assert.Equal(1000, repo.SalesList[repo.SalesList.Count - 1].SaleAmount);
        }

        [Fact]
        public void CalcTotalSalesTest()
        {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo);

            //Act
            Sale s = new Sale() { SaleAmount = 1000, saleDate = DateTime.Today };
            Sale s2 = new Sale() { SaleAmount = 2000, saleDate = DateTime.Today };
            repo.AddSale(s);
            repo.AddSale(s2);

            //Assert
            Assert.Equal(2, repo.GetSalesCount());
            Assert.Equal(1000, repo.SalesList[0].SaleAmount);
            Assert.Equal(2000, repo.SalesList[repo.SalesList.Count - 1].SaleAmount);
            Assert.Equal(3000, repo.CalcTotalSales());
        }
    }
}
