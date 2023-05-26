using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Areas.AdminPanel.ViewModels.AboutPeople
{
    public class UpdateAboutPeopleVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [AllowNull]
        public IFormFile Photo { get; set; }
    }
}
