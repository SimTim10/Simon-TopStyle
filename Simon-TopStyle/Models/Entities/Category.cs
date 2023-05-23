using System.ComponentModel.DataAnnotations;

namespace Simon_TopStyle.Models.Entities
{
    public class Category
    {
        [Key]
        public int CategortyId { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
