using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.Domain.Models
{
    public class ExcerptCard
    {
        [Key]
        public int ExcerptCardId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1600)]
        public string Excerpt { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        public int CitationId { get; set; }

        public Citation Citation { get; set; }

    }
}
