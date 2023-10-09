using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.Domain.Models
{
    public class ExcerptCardKeyword
    {
        [Required, Key]
        public int ExcerptCardId {  get; set; }

        [Required, Key]
        public int KeywordId { get; set; }
    }
}
