using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.Domain.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime LastUpdateDate { get; set; }

        [Required, Key]
        public int UserProfileId { get; set; }

    }
}
