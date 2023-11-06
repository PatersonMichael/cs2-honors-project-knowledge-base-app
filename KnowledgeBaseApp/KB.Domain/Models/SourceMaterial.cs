using KB.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.Domain.Models
{
    public class SourceMaterial
    {
        [Key]
        public int SourceMaterialId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }


        public string? Publisher { get; set; }

        [Required]
        public string SourceMaterialType { get; set; }

        public string? SourceMaterialEdition { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        //public List<Author> Authors { get; }

        //public List<SourceMaterialAuthor> SourceMaterialsAuthors { get; } = new();


    }
}
