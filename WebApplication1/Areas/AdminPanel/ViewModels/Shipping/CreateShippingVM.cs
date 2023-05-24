using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Shipping;

public class CreateShippingVM
{
    public string Label { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public IFormFile Photo { get; set; }
}
