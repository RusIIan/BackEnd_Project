namespace WebApplication1.Models
{
    public class HomeDescription : BaseEntity<int>
    {
        public string? ProductDescription { get; set; }
        public string? TestimonialDescription { get; set; }
        public string? BlogDescription { get; set; }
    }
}
