using Microsoft.EntityFrameworkCore;
using Simon_TopStyle.Data.DataModels;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly TopStyleDBContext _dbContext;

        public CustomerRepo(TopStyleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllProducs()
        {
            var productList = await _dbContext.Products.ToListAsync();
            return productList;
        }

        public async Task<List<Product>> SearchProducts(Product product)
        {
            var searchResult = await _dbContext.Products.
                Where(p=> p.ProductName.Contains(product.ProductName)).
                Include(p => p.Category)
                .ToListAsync();
            return searchResult;
        }
        
    }
}
