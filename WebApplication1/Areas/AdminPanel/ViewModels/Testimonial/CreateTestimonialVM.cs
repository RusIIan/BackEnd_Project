using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Testimonial;

public class CreateTestimonialVM
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Comment { get; set; }
    public string Client { get; set; }
    [Required]
    public IFormFile Photo { get; set; }

}
