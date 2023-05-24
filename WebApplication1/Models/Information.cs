namespace WebApplication1.Models
{
    public class Information:BaseEntity<int>
    {
        public string Shipping { get; set; } = string.Empty;
        public string AboutReturnRequest { get; set; } = string.Empty;
        public string Guarate { get; set; } = string.Empty;
        public string Descripton { get; set; } = string.Empty;  
    }
}
