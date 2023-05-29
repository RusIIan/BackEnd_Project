namespace WebApplication1.Models
{
    public class ProductColor:BaseEntity<int>
    {
        public string Name { get; set; }
        public int? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
