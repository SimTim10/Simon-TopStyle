using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Data.Repos
{
    public class RolesRepo : IRolesRepo
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RolesRepo> _logger;

        public RolesRepo(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ILogger<RolesRepo> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public List<IdentityRole> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();

            return roles;
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
          }

        public async Task<IdentityResult> CreateRole(string roleName)
        {
            // Check if role Exists
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The role {roleName} has been added Successfully");
                    return roleResult;
                    //throw new Exception ($"The role {roleName} has been added") ;
                }
            }
            throw new Exception("Role already exists");
        }

        public async Task<IdentityResult> AddUserToRole(IdentityUser user,string roleName)
        {

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result;
            
        }

        public async Task<IList<string>> GetUserRole(IdentityUser user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }
    }
}
