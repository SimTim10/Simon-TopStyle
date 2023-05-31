using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Data.Interfaces
{
    public interface IAdminRepo
    {
        public void AddNewProduct(ProductDTO product,int categoryId);
        public List<Product> GetProducts();
    }
}
