using KB.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.Domain.Models
{
    public class Citation
    {
        [Key]
        public int CitationId { get; set; }

        [Required]
        public Formats Format {  get; set; }

        public string? ExcerptLocation { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        [Required]
        public int SourceMaterialId { get; set; }
    }
}
