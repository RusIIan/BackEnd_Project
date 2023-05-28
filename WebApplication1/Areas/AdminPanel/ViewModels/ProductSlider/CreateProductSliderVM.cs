using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.ProductSlider
{
    public class CreateProductSliderVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Star { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
