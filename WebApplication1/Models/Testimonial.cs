namespace WebApplication1.Models
{
    public class Testimonial:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
        public string Client { get; set; }
    }
}
