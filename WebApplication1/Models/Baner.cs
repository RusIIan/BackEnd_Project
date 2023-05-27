namespace WebApplication1.Models
{
    public class Baner:BaseEntity<int>
    {   
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
