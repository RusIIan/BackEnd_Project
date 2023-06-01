using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Blog
{
    public class CreateBlogVM
    {
        [Required]
        public string BlogTitle { get; set; }
        [Required]
        public string BlogDescription { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
