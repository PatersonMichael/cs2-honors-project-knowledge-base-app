namespace KB.Web.API.DtoModels
{
    public class ExcerptCard
    {
        public int ExcerptCardId { get; set; }

        public string Title { get; set; }

        public string Excerpt { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public int UserProfileId { get; set; }

        public Citation Citation { get; set; }

        public int CitationId { get; set; }

        public List<Keyword> Keywords { get; set; }

    }
}
