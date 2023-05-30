using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class LoginVM
    {
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public string UserName { get; set; }
    }
}
