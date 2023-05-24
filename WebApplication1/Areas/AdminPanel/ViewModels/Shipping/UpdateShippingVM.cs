using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Shipping;

public class UpdateShippingVM
{
    public int Id { get; set; }
    [Required]
    public string Label { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public IFormFile Photo { get; set; }
    [AllowNull]
    public string Image { get; set; }
}
