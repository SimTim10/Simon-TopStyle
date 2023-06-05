using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Models.DTOs
{
    public class ProductDTO
    {
        //public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        //public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }
        public string CategoryName { get; set; }
                
    }
}
