using SourceMaterialTypes = KB.Web.API.DtoModels.Enums.SourceMaterialTypes;

namespace KB.Web.API.DtoModels
{
    public class SourceMaterial
    {
        public int SourceMaterialId { get; set; }

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }


        public string? Publisher { get; set; }

        public SourceMaterialTypes SourceMaterialType { get; set; }

        public string? SourceMaterialEdition { get; set; }

        public int UserProfileId { get; set; }
    }
}
