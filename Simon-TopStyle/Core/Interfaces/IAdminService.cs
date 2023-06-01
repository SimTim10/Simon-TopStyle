using Microsoft.AspNetCore.Identity;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Core.Interfaces
{
    public interface IAdminService
    {
        Task AddNewProduct(ProductDTO product);
        public List<Product> GetProducts();
    }
}
