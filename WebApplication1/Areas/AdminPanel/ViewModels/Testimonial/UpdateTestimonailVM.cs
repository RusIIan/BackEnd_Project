using Microsoft.AspNetCore.Http;

namespace WebApplication1.Areas.AdminPanel.ViewModels.Testimonial
{
    public class UpdateTestimonailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Comment { get; set; }
        public string Client { get; set; }
        public string Image { get; set; }
        
        public IFormFile Photo { get; set; }
    }
}
