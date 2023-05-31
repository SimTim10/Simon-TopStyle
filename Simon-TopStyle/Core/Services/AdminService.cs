using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminService(IAdminRepo adminRepo, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _adminRepo = adminRepo;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public List<Product> GetProducts()
        {
            return _adminRepo.GetProducts();
        }
        public void AddNewProduct(ProductDTO product,int categoryId)
        {
            _adminRepo.AddNewProduct(product, categoryId);
        }
    }
}
