using System.ComponentModel.DataAnnotations;

namespace HealthCenter.Models.ViewModels
{
    public class PatientViewModel
    {
        [Required]
        public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
