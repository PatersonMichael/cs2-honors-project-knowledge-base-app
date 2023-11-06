using KB.Domain.Models;
using SourceMaterialTypes = KB.Web.API.DtoModels.Enums.SourceMaterialTypes;

namespace KB.Web.API.DtoModels
{
    public class SourceMaterial
    {
        public int SourceMaterialId { get; set; }

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }


        public string? Publisher { get; set; }

        public string SourceMaterialType { get; set; }

        public string? SourceMaterialEdition { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public int UserProfileId { get; set; }

        //public List<Author> Authors { get; set; }

    }
}
