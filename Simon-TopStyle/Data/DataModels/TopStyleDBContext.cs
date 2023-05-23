using Microsoft.EntityFrameworkCore;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Data.DataModels
{
    public class TopStyleDBContext : DbContext
    {
        public TopStyleDBContext(DbContextOptions<TopStyleDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Product>()
            //    .HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.Category);
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
 
    }
}
