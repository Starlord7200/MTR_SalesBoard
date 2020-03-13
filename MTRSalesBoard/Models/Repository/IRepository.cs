using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTRSalesBoard.Models.Repository
{
    public interface IRepository
    {
        List<AppUser> UsersList { get; }
        void AddUser(AppUser u);
        void AddSale(Sale s);
        int GetSalesCount();
        decimal CalcTotalSales();
    }
}
