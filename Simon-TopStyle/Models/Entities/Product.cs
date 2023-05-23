using System.ComponentModel.DataAnnotations;

namespace Simon_TopStyle.Models.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        //public virtual Category Category { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
        
    }
}
