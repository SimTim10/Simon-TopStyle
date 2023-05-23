using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Simon_TopStyle.Models.Entities;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Data.DataModels
{
    public class TopStyleDBContext : IdentityDbContext<ApplicationUser>
    {
        public TopStyleDBContext(DbContextOptions<TopStyleDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductOrder>().HasKey(po => new {po.ProductId,po.OrderId});
            //modelBuilder.Entity<Product>()
            //    .HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.Category);
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductsOrders { get; set;}
    }
}
