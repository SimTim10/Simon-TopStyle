using System.ComponentModel.DataAnnotations;

namespace Simon_TopStyle.Models.Entities
{
    public class Order
    {
        [Key]

        public int OrderId { get; set; }
        [Required]
        [StringLength(200)]
        public string ShippingAddress { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int ProductsCount { get; set; }

        public Customer Customer { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }

        //public List<Product> Products { get; set; }
    }
}
