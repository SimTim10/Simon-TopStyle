using System.ComponentModel.DataAnnotations;

namespace Simon_TopStyle.Models.Entities
{
    public class Order
    {
        [Key]

        public int OrderId { get; set; }

        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
    }
}
