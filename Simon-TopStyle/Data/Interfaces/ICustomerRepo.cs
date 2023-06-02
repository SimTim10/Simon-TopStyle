using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<List<Product>> GetAllProducs();
        Task<List<Product>> SearchProducts(Product product);
        Task<Order> SetOrder(Order order);
        Task<Customer> GetMyInfo(string email);
    }
}
