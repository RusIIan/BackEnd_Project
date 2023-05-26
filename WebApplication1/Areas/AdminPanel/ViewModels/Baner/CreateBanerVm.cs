using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Baner
{
    public class CreateBanerVm
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
