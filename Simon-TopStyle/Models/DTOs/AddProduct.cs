namespace Simon_TopStyle.Models.DTOs
{
    public class AddProduct
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailabe { get; set; }
    }
}
