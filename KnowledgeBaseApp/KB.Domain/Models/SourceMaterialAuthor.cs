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
        [Required, Key]
        public int SourceMaterialId { get; set; }

        [Required, Key]
        public int AuthorId { get; set; }
    }
}
