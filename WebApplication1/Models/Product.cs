namespace WebApplication1.Models
{
    public class Product: BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Start { get;set; }

    }
}
