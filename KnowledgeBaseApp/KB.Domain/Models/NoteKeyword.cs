using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KB.Domain.Models
{
    [PrimaryKey(nameof(NoteId), nameof(KeywordId))]
    public class NoteKeyword
    {
        [Required]
        public int NoteId { get; set; }

        [Required]
        public int KeywordId { get; set; }
    }
}
