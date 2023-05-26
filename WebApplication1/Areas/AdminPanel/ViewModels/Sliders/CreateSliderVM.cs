using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Sliders;

public class CreateSliderVM
{
    [Required]
    public string Precent { get; set; }
    [Required]
    public string Label { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public IFormFile Photo { get; set; }
}
