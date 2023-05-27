namespace WebApplication1.Models
{
    public class Contact : BaseEntity<int>
    {
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; } 
        public string Description { get; set; }
    }
}
