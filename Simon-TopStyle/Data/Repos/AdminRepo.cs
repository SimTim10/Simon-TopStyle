using Simon_TopStyle.Data.DataModels;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Data.Repos
{
    public class AdminRepo
    {
        private readonly TopStyleDBContext _dbContext;

        public void AddNewProduct(AddProduct product)
        {
            var newProduct = new Product()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price

            };
        }
    }
}
