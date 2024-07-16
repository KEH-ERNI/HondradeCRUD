using System.ComponentModel.DataAnnotations;

namespace HondradeCRUD.Models
{
    public class BootcamperDTO
    {

        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        public IFormFile ImageFile { get; set; }

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; } = "";

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string Phone { get; set; }

        [Required]
        public DateTime Birthday { get; set; } 

        public int Age { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; } = "";

        [Required, MaxLength(10)]
        public string Gender { get; set; } = "";

    }
}
