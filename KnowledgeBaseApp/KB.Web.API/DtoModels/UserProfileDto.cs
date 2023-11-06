namespace KB.Web.API.DtoModels
{
    public class UserProfileDto
    {
        public int UserProfileId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime BirthDate { get; set; }

        public string Nametag { get; set; }

        //public List<Author> Authors { get; set; }
    }
}
