using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Baner
{
    public class CreateBanerSliderVm
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
