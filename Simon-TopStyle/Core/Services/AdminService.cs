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
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminService(IAdminRepo adminRepo, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _adminRepo = adminRepo;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public List<IdentityRole> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();

            return roles;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return users;
        }

        public void AddNewProduct(AddProduct product)
        {
            _adminRepo.AddNewProduct(product);
        }
    }
}
