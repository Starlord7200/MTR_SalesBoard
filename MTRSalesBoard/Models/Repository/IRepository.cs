using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models.Repository
{
    public interface IRepository
    {
        List<AppUser> Users { get; }
        List<Sale> Sales { get; }
        void AddUser(AppUser u);
        AppUser FindAppUserbyName(string Name);
        void AddSale(Sale s, AppUser u);
        int GetSalesCount();
        decimal CalcTotalSales();
        Sale FindSaleById(int id);

        public int EditSale(Sale s);
    }
}
