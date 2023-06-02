using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class ProductStyleVM
    {
        public List<ProductStylePage> productStylePages { get; set; }
        public ProductStylePage productStylePage { get; set; }
        public Category Category { get; set; }
        public Product homeProduct { get; set; }
        public List<Product> homeProducts { get; set; }
    }
}
