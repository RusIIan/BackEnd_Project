using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Baner
{
    public class UpdateBanerVM
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        [AllowNull]
        public string Image { get; set; }
    }
}
