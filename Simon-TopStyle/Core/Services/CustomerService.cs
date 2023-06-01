using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Data.DataModels;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly TopStyleDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        
        public CustomerService(ICustomerRepo customerRepo, TopStyleDBContext dbContext, UserManager<IdentityUser> userManager)
        {
            _customerRepo = customerRepo;
            _dbContext = dbContext;
            _userManager = userManager;
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
        public async Task<Customer> GetCustomer(string email)
        {
            var user = new IdentityUser()
            {
                Email = email

            };
            //Check if email exists.
            var checkEmail = await _userManager.GetEmailAsync(user);
            if (checkEmail == null)
            {
                throw new Exception("Email not found! Please check your email!");
            }
            var customer = await _customerRepo.GetMyInfo(email);
            return customer;
        }
    }
}
