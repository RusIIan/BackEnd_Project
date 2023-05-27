using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Baner;

public class CreateBanerVM
{
    public string Label { get; set; }
    public string Description { get; set; }
    [Required]
    public IFormFile Photo { get; set; }
}
