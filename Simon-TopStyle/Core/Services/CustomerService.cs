using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        
        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var productList = await _customerRepo.GetAllProducs();
            return productList;
        }

        public async Task<List<ProductDTO>> SearchProduct(string searchInput)
        {
            var product = new Product()
            {
                ProductName = searchInput
            };
            //Check if a product with the same name already exists.
            var resultProducts = await _customerRepo.SearchProducts(product);
            if (resultProducts.Count<=0)
            {
                throw new Exception("No product match your search.");
            }
            
            var productDtos = new List<ProductDTO>();
            foreach(var resultProduct in resultProducts)
            {
                var productDto = new ProductDTO()
                {
                    ProductName = resultProduct.ProductName,
                    Description = resultProduct.Description,
                    Price = resultProduct.Price,
                    //CategoryId = resultProduct.CategoryId,
                    IsAvailabe = resultProduct.IsAvailable,
                    CategoryName = resultProduct.Category.CategoryName 
                    
                };
                productDtos.Add(productDto);
            }
            return productDtos;
        }
    }
}
