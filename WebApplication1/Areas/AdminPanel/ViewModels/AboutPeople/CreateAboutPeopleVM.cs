using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.AboutPeople
{
    public class CreateAboutPeopleVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
