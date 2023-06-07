using Microsoft.EntityFrameworkCore;
using Simon_TopStyle.Data.DataModels;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Data.Repos
{
    public class AdminRepo : IAdminRepo
    {
        private readonly TopStyleDBContext _dbContext;

        public AdminRepo(TopStyleDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Order>> GetOrders()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return orders;
        }
        public List<Product> GetProducts()
        {
            var allProducts = _dbContext.Products.ToList();
            return allProducts;
        }

        public async Task AddNewProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            
           await _dbContext.SaveChangesAsync();
        }

        public async Task EditProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }

        public async Task DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public async Task EditOrder(ProductOrder productOrder,bool delete)
        {
            if(delete)
            {
                _dbContext.ProductsOrders.Remove(productOrder);
                _dbContext.SaveChanges();
            }
            else if(!delete)
            {
                _dbContext.ProductsOrders.Add(productOrder);
                _dbContext.SaveChanges();
            }
            
        }
    }
}
