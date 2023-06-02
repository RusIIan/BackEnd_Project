using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.ProductStypePage
{
    public class CreateProductStylePageVM
    {
   
        [Required,MaxLength(100)]
        public string ShippingText { get; set; } = string.Empty;
        [Required,MaxLength(25)]

        public string AboutreturnrequestText { get; set; } = string.Empty;
        [Required,MaxLength(100)]
        public string Guaranteetext { get; set; } = string.Empty;
        [Required,MaxLength(150)]
        public string Description { get; set; } = string.Empty;
        [Required,MaxLength(15)]
        public string ClientName { get; set; } = string.Empty;
        [Required,MaxLength(200)]
        public string ClientComent { get; set; } = string.Empty;
        [Required]
        public IFormFile Photo { get; set; }
    }
}
