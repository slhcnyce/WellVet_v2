using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellVet_v2.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsVeterinarian { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
