using System.ComponentModel.DataAnnotations;

namespace KB.Web.API.DtoModels
{
    public class Keyword
    {
        public int KeywordId { get; set; }

        public string Name { get; set; }

        public int UserProfileId { get; set; }
    }
}
