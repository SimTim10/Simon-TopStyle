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
                    IsAvailable = resultProduct.IsAvailable,
                    CategoryName = resultProduct.Category.CategoryName 
                    
                };
                productDtos.Add(productDto);
            }
            return productDtos;
        }
        public async Task<Order> SetOrder(List<int> productIdList, OrderDTO OrderDTO)
        {
            var productList = new List<Product>();
            var productOrder = new List< ProductOrder>();
            
            //Check if products exist.
            foreach (var id in productIdList)
            {
                var checkProductList = await _dbContext.Products.SingleOrDefaultAsync(p => p.ProductId == id);
                if (checkProductList == null)
                {
                    throw new Exception($"Product not found/or unavailable: {id}");
                }

                //Gather all products
                var getProduct = await _dbContext.Products.SingleOrDefaultAsync(p => p.ProductId == id);
                productList.Add(getProduct);
            }
            //Get customer.
            var customer = await _dbContext.Customers
                .SingleOrDefaultAsync(c => c.Email == OrderDTO.Email);
            if(customer == null)
            {
                throw new Exception("User not found! Please check your email.");
            }

            //Create Order
            var order = new Order()
            {
                ShippingAddress = OrderDTO.ShippingAddress,
                Price = productList.Sum(p => p.Price),
                ProductsCount = productList.Count,
                CustomerId = customer.CustomerId
            };

            var createdOrder = await _customerRepo.SetOrder(order);
            foreach (var product in productList)
            {
                var po = new ProductOrder()
                {
                    ProductId = product.ProductId,
                    OrderId = createdOrder.OrderId,
                };
                await _dbContext.ProductsOrders.AddAsync(po);
                await _dbContext.SaveChangesAsync();
            }

            return createdOrder;
        }
        public async Task<Customer> GetUser(string email)
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
