using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.DTOs;
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


        

        public void AddNewProduct(AddProduct product)
        {
            _adminRepo.AddNewProduct(product);
        }
    }
}
