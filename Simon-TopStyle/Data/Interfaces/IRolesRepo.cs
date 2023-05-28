using Microsoft.AspNetCore.Identity;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Data.Interfaces
{
    public interface IRolesRepo 
    {
        public List<IdentityRole> GetAllRoles();
        Task<List<IdentityUser>> GetAllUsers();
        Task<IdentityResult> CreateRole(string roleName);
        Task<IdentityResult> AddUserToRole(IdentityUser user, string roleName);
        Task<IList<string>> GetUserRole(IdentityUser user);
        Task RemoveRole(IdentityUser user, string roleName);
    }
}
