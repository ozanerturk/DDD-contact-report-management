namespace API.Controllers
{
    public class ContactInformationDTO
    {
        public int id {get;set;}
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
        public string description { get; set; }
        public string location { get; set; }
    }
}