using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models.Repository
{
    public interface IRepository
    {
        List<Sale> Sales { get; }
        void AddUser(AppUser u);
        void AddSale(Sale s, AppUser u);
        int GetSalesCount();
        decimal CalcTotalSales();
        Sale FindSaleById(int id);
        int EditSale(Sale s);
        void DeleteSale(int id);
        void DeleteUser(AppUser u);
        decimal CalcMonthYearSales(decimal month, decimal year);
        decimal CalcMonthLastYearSales();
    }
}
