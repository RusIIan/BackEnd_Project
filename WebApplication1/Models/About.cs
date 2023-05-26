namespace WebApplication1.Models
{
    public class About : BaseEntity<int>
    {
        public string Title { get; set;  } = string.Empty;
        public string VideoLink { get; set; } = string.Empty;
        public string UpDescription { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }   
}
