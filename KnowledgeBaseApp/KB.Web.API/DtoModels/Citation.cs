using KB.Web.API.DtoModels.Enums;

namespace KB.Web.API.DtoModels
{
    public class Citation
    {
        public int CitationId { get; set; }

        public string Format { get; set; }

        public string? ExcerptLocation { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserProfileId { get; set; }

        public int SourceMaterialId { get; set; }
    }
}
