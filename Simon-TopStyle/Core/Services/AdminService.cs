using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Data.DataModels;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;
        private readonly TopStyleDBContext _dbContext;

        public AdminService(IAdminRepo adminRepo,TopStyleDBContext dbContext)
        {
            _adminRepo = adminRepo;
            _dbContext = dbContext;
        }

        public List<Product> GetProducts()
        {
            return _adminRepo.GetProducts();
        }
        public async Task AddNewProduct(ProductDTO product)
        {
            var newProduct = new Product()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                IsAvailable = product.IsAvailable,
                                
            };
            
            //Check if a product with the same name already exists.
            var checkProduct = _dbContext.Products
                .Any(p => p.ProductName == newProduct.ProductName);
            if (checkProduct)
            {
                throw new Exception("A product with the same name already exists!");
            }
            //Check if category already exists.
            var checkCategory = _dbContext.Categories
                .SingleOrDefault(c => c.CategoryName == product.CategoryName);
            //if category doesn't exist, first create new category then add category id to new product then add product to DB
            if(checkCategory == null)
            {
                var newCategory = new Category()
                {
                    CategoryName = product.CategoryName
                };
                _dbContext.Categories.Add(newCategory);
                _dbContext.SaveChanges();
                newProduct.CategoryId = newCategory.CategortyId;
                await _adminRepo.AddNewProduct(newProduct);                
            }
            //if category exists, add category id to new product then add it to DB.
            else
            {
                newProduct.CategoryId = checkCategory.CategortyId;
                await _adminRepo.AddNewProduct(newProduct);
            }
        }
        public async Task EditProduct(int productId,ProductDTO productDTO)
        {

            //Check if a product with the same name already exists.
            var checkProduct = await _dbContext.Products
                .SingleOrDefaultAsync(p => p.ProductId == productId);
            if (checkProduct == null)
            {
                throw new Exception("Product was not found. Please check your Product Id");
            }
            //var newProduct = new Product()
            //{
            //    ProductId = productId,
            //    ProductName = productDTO.ProductName,
            //    Description = productDTO.Description,
            //    Price = productDTO.Price,
            //    IsAvailable = productDTO.IsAvailable
            //};

            //checkProduct.ProductId = productId;
            checkProduct.ProductName = productDTO.ProductName;
            checkProduct.Description = productDTO.Description;
            checkProduct.Price = productDTO.Price;
            checkProduct.IsAvailable = productDTO.IsAvailable;

            //Check if category already exists.
            var checkCategory = _dbContext.Categories
                .SingleOrDefault(c => c.CategoryName == productDTO.CategoryName);
            //if category doesn't exist, first create new category then add category id to new product then add product to DB
            if (checkCategory == null)
            {
                var newCategory = new Category()
                {
                    CategoryName = productDTO.CategoryName
                };
                _dbContext.Categories.Add(newCategory);
                _dbContext.SaveChanges();
                checkProduct.CategoryId = newCategory.CategortyId;
                await _adminRepo.EditProduct(checkProduct);
            }
            //if category exists, add category id to new product then add it to DB.
            else
            {
                checkProduct.CategoryId = checkCategory.CategortyId;
                await _adminRepo.EditProduct(checkProduct);
            }
            

        }
        public async Task DelProduct( int productId)
        {
            //Check product exists.
            var Product = _dbContext.Products.SingleOrDefault(p => p.ProductId == productId);
            if (Product == null)
            {
                throw new Exception($"Product {productId} was not found, check your ProductId");
            }
            //chek if product exists in an order.
            var checkProduct = _dbContext.ProductsOrders.Any(pO => pO.ProductId == productId);
            if (!checkProduct)
            {
                await _adminRepo.DeleteProduct(Product);
            }
            else
            {
                throw new Exception("Cannot delete this product, it is a part of an order!");
            }
        }
        
        public async Task EditOrder(int productId, int orderId,bool delete)
        {
            //Check Order Exists.
            var order = _dbContext.Orders.SingleOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception($"Order {orderId}, does not exist!");
            }
            //var productOrders = new List<ProductOrder>();
            //Get productOrderList.
            var productOrders = await _dbContext.ProductsOrders
                .Where(pO => pO.OrderId == orderId).ToListAsync();
            if(delete)
            {
                foreach (var productOrder in productOrders)
                {
                    var oPobject = await _dbContext.ProductsOrders.SingleOrDefaultAsync(oP => oP.ProductId == productId);
                    if (oPobject != null)
                    {
                        await _adminRepo.EditOrder(oPobject,delete);
                    }
                }
            }
            else if(!delete)
            {
                //Get productOrder obj.
                
                //Get product obj.
                var product = await _dbContext.Products
                    .SingleOrDefaultAsync(p => p.ProductId == productId);
                var productOrder = new ProductOrder()
                {
                    OrderId = orderId,
                    ProductId = productId
                };
                
                await _adminRepo.EditOrder(productOrder, delete);
                
            }
        }
    }
}
