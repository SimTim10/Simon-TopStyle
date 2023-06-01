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
                IsAvailable = product.IsAvailabe,
                                
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

            //newProduct.Description = product.Description;
            //newProduct.Price = product.Price;
            //newProduct.Category.CategortyId = product.CategoryId;
            //newProduct.IsAvailable = product.IsAvailabe;
            

            //var newProduct = new Product()
            //{

            //    ProductName = product.ProductName,
            //    Description = product.Description,
            //    Price = product.Price,
            //    CategoryId = product.CategoryId,
            //    IsAvailable = product.IsAvailabe
                
            //};
            ////Check if product already exists.
            //var checkProduct = await _dbContext.Products
            //    .AnyAsync(p => p.ProductName == newProduct.ProductName);
            //if (checkProduct)
            //{
            //    throw new Exception("Product with the same name already exist");
            //}
            ////Check if product category already exists.
            ///*var checkCategoryN = await _dbContext.Categories
            //    .AnyAsync(c => c.CategoryName)*/
            //var checkCategory = await _dbContext.Categories
            //    .AnyAsync(c => c.CategortyId == newProduct.CategoryId);
            //if (!checkCategory)
            //{
            //    throw new Exception("No matching category id was found.");
            //}

            //await _adminRepo.AddNewProduct(newProduct);
        }
    }
}
