using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<List<Product>> GetAllProducs();
    }
}
