using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HondradeCRUD.Models
{
    public class Bootcamper
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";

        [MaxLength(100)]
        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public int Age { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }
    }
}
