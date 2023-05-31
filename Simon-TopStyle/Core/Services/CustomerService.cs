using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var productList = await _customerRepo.GetAllProducs();
            return productList;
        }
    }
}
