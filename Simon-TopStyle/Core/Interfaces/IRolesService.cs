using Microsoft.AspNetCore.Identity;

namespace Simon_TopStyle.Core.Interfaces
{
    public interface IRolesService
    {
        public List<IdentityRole> GetAllRoles();
        Task<List<IdentityUser>> GetAllUsers();
        Task<IdentityResult> CreateRole(string newRole);

        Task<IdentityResult> AddUserToRole(string email, string roleName);
    }
}
