using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class ProductSlider : BaseEntity<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Star { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
