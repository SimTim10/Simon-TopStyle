using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;

        public AdminService(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
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
