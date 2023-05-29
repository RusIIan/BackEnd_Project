namespace WebApplication1.Models
{
    public class QuickLink :BaseEntity<int>
    {
        public string Description { get; set; }

        public int PhoneNumber { get; set; }

    }
}
