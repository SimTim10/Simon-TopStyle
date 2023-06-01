using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Product>> GetAllProducts();
        Task<List<ProductDTO>> SearchProduct(string searchInput);
        Task<Customer> GetCustomer(string email);
    }
}
