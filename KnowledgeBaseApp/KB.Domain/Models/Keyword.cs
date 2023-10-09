using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.Domain.Models
{
    public class Keyword
    {
        [Key]
        public int KeywordId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required, Key]
        public int UserProfileId { get; set; }
    }
}
