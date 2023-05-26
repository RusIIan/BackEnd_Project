using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Shipping : BaseEntity<int>
{
    [Required]
    public string Label { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Image { get;set; }
    [NotMapped]
    public IFormFile Photo { get; set; }
}
