namespace WebApplication1.Models
{
    public class HomeProduct: BaseEntity<int>
    {
        public string Name { get; set; } 
        public decimal Price { get; set; }
        public decimal Star { get;set; }
        public string Image { get; set; } 
        public int CategoryId { get; set; }
        //navigation
        public virtual HomeCategory Category { get; set; }
    }
}
