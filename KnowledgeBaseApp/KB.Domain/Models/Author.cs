using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.Domain.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        public UserProfile UserProfile { get; set; }

        [Required]
        public int UserProfileId { get; set; }
    }
}
