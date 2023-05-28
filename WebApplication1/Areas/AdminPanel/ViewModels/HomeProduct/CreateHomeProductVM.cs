using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.HomeProduct
{
    public class CreateHomeProductVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Star { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
