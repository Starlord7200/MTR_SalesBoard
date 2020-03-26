using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MTRSalesBoard.Models.Repository
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(
            DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
