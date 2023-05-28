using Microsoft.AspNetCore.Identity;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Core.Interfaces
{
    public interface IAdminService
    {
        public List<IdentityRole> GetAllRoles();
        Task<List<ApplicationUser>> GetAllUsers();
        public void AddNewProduct(AddProduct product);
    }
}
