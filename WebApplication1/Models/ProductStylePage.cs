namespace WebApplication1.Models
{
    public class ProductStylePage : BaseEntity<int>
    {
        public string ShippingText { get; set; } = string.Empty;
        public string AboutreturnrequestText { get; set; } = string.Empty;
        public string Guaranteetext { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientComent { get; set; } = string.Empty;
        public int CommentCount { get; set; } = 1;
        public string Image { get; set; } = string.Empty;
    }
}
