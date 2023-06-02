namespace WebApplication1.Models
{
    public class ProductColor:BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; }
    }
}
