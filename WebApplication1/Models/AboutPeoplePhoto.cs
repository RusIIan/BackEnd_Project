namespace WebApplication1.Models
{
    public class AboutPeoplePhoto : BaseEntity<int>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
