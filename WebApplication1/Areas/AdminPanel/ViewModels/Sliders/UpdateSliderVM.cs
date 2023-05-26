using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Sliders
{
    public class UpdateSliderVM
    {
        public int Id { get; set; }
        public string Precent { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        [AllowNull]
        public string Image { get; set; }
    }
}
