using Microsoft.AspNetCore.Identity;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Core.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepo _rolesRepo;
       

        public RolesService(IRolesRepo rolesRepo)
        {
            _rolesRepo = rolesRepo;
        }

        public List<IdentityRole> GetAllRoles()
        {
            var roles = _rolesRepo.GetAllRoles();
            return roles;
        }

        public async Task< List<IdentityUser>> GetAllUsers()
        {
            var users = await _rolesRepo.GetAllUsers();
            return users;
        }

        public async Task<IdentityResult> CreateRole(string newRole)
        {

            var addRole = await _rolesRepo.CreateRole(newRole);
            return addRole;
            
        }
    }
}
