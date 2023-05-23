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
        public string type { get; set; }
        public Category Category { get; set; }
    }
}
