using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Product>> GetAllProducts();
    }
}
