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

        public List<Product> GetProducts()
        {
            var allProducts = _dbContext.Products.Include(p => p.Category)
                .ToList();
            return allProducts;
        }

        public void AddNewProduct(ProductDTO product,int categoryId)
        {
            var newProduct = new Product()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                IsAvailable = product.IsAvailabe
            };
            _dbContext.Products.Add(newProduct);
            
            _dbContext.SaveChanges();
        }
    }
}
