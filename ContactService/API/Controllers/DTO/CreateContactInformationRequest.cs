namespace API.Controllers
{
    public class CreateContactInformationRequest
    {
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
        public string description { get; set; }
        public string location { get; set; }
    }
}