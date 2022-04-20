using Microsoft.EntityFrameworkCore;


namespace Api.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=ALI\SQLEXPRESS15;Initial Catalog=ShopsRUs;Integrated Security=True; Connection Timeout=60");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceStatus> InvoicesStatus { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}
