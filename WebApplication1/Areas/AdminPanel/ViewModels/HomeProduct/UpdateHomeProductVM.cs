using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.HomeProduct;

    public class UpdateHomeProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Star { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
         public string Image { get; set; }
    }

