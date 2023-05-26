using Microsoft.AspNetCore.Http;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Areas.AdminPanel.ViewModels.About;

public class CreateAboutUpVM
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string VideoLink { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; }
    [Required]
    public IFormFile Photo { get; set; }

}