using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Core.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepo _rolesRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RolesService> _logger;
       

        public RolesService(IRolesRepo rolesRepo, UserManager<IdentityUser> userManager, ILogger<RolesService> logger, RoleManager<IdentityRole> roleManager)
        {
            _rolesRepo = rolesRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
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

        public async Task<IdentityResult> AddUserToRole(string email,string roleName)
        {
            //Check if user exists
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null) 
            {
                _logger.LogInformation($"No user found. Check email again!");
                throw new Exception("User was not found!");
            }
            //Check if role exists
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                _logger.LogInformation($"{roleName} does not exist!");
                throw new Exception("Role was not found!");
            }

            var result = await _rolesRepo.AddUserToRole(user, roleName);
            return result;
            
        }
        public async Task<IList<string>> GetUserRole(string email)
        {
            // Check user exists
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"No user was found!");
                throw new Exception("Email is invalid!");
            }
            var listOfRoles = await _rolesRepo.GetUserRole(user);
            if (listOfRoles.IsNullOrEmpty())
            {
                throw new Exception("User has no roles!");
            }
            return listOfRoles;
        }
    }
}
