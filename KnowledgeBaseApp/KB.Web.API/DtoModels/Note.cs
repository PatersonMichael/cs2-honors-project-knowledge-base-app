﻿namespace KB.Web.API.DtoModels
{
    public class Note
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public int UserProfileId { get; set; }

        public List<Keyword> Keywords { get; set; }

    }
}
