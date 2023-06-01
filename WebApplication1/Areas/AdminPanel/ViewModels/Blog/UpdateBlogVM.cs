using Microsoft.AspNetCore.Http;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Blog
{
    public class UpdateBlogVM
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set;}
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
