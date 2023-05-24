using WebApplication1.Models;

public class Slider:BaseEntity<int>
{
    public decimal Percent { get; set; }
    public string Label { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}
