using Ness_customers_server.Models;
using Microsoft.EntityFrameworkCore;
namespace Ness_customers_server.Data
{
    public class CustomerApiDbContext: DbContext
    {
        public CustomerApiDbContext(DbContextOptions options) : base(options) {

        }

        public DbSet <Customer> Customers { get; set; }
    }
}
