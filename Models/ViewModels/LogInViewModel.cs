using System.ComponentModel.DataAnnotations;

namespace HealthCenter.Models.ViewModels
{
    public class LogInViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
