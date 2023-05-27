using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Baner
{
    public class UpdateBanerSliderVM
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        [AllowNull]
        public IFormFile Photo { get; set; }
    }
}
