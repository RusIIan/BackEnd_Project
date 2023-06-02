using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Areas.AdminPanel.ViewModels.ProductStypePage
{
    public class UpdateProductStylePageVM
    {
        public int id { get; set; }
        public string ShippingText { get; set; } = string.Empty;
        public string AboutreturnrequestText { get; set; } = string.Empty;
        public string Guaranteetext { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientComent { get; set; } = string.Empty;
        public IFormFile Photo { get; set; }
        [AllowNull]
        public string Image { get; set; } = string.Empty;
    }
}
