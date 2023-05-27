namespace WebApplication1.Models
{
    public class Blog :BaseEntity<int>
    {
        public string Description { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set; }
        public string Image { get; set; }
    }
}
