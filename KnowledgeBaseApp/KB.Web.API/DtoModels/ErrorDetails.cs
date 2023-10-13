namespace KB.Web.API.DtoModels
{
    // Catches error statuscodes and messages for consistent messaging
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
