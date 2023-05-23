using System.ComponentModel.DataAnnotations;

namespace Simon_TopStyle.Models.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(80)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(80)]
        public string Email { get; set; }
        public List<Order> Orders { get; set; }
    }
}
