using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KB.Domain.Models
{
    internal class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(320)]
        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        public DateOnly BirthDate { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        [Required]
        [MaxLength(60)]
        public string Nametag { get; set; }
    }
}
