using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Areas.AdminPanel.ViewModels.ProductSlider
{
    public class UpdateProductSliderVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Star { get; set; }
        public IFormFile Photo { get; set; }
        [AllowNull]
        public string Image { get; set; }
    }
}
