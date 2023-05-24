using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Sliders
{
    public class UpdateSliderVM
    {
        public int Id { get; set; }
        [Required]
        public decimal Precent { get; set; }
        [Required]
        public string Label { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [AllowNull]
        public string Image { get; set; }
    }
}
