namespace WebApplication1.Areas.AdminPanel.ViewModels.HomeDescription
{
    public class UpdateHomeDescriptionVM
    {
        public int id { get; set; }
        public string? ProductDescription { get; set; } = string.Empty;
        public string? TestimonialDescription { get; set; } = string.Empty;
        public string? BlogDescription { get; set; } = string.Empty;
    }
}
