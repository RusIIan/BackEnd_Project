using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Shipping : BaseEntity<int>
{
    [Required]
    public string Label { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Image { get;set; }
}
