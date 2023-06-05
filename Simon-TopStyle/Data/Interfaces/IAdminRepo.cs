using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Data.Interfaces
{
    public interface IAdminRepo
    {
        Task AddNewProduct(Product product);
        public List<Product> GetProducts();
        Task EditProduct(Product product);
        Task DeleteProduct(Product product);
        Task EditOrder(ProductOrder productOrder,bool delete);
    }
}
