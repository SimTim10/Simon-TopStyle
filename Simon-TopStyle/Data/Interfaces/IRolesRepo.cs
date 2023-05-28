using Microsoft.AspNetCore.Identity;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Data.Interfaces
{
    public interface IRolesRepo 
    {
        public List<IdentityRole> GetAllRoles();
        Task<List<IdentityUser>> GetAllUsers();
        Task<IdentityResult> CreateRole(string roleName);
    }
}
