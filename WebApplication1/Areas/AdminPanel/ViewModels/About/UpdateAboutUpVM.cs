using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Areas.AdminPanel.ViewModels.About
{
    public class UpdateAboutUpVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public IFormFile Photo { get; set; }
        [AllowNull]
        public string image { get; set; }
    }
}
