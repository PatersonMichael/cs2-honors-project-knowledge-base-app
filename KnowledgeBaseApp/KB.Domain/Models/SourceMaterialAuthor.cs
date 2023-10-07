using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.Domain.Models
{
    internal class SourceMaterialAuthor
    {
        [Required]
        public int SourceMaterialId { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
