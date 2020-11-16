using System.Collections.Generic;

namespace API.Controllers
{
    public class PersonDetailedDTO
    {
        public int id { get; set; }
        public string identityGuid { get; set; }

        public string name { get; set; }
        public string surname { get; set; }
        public string company { get; set; }
        public List<ContactInformationDTO> contactInformations { get; set; }

        public PersonDetailedDTO()
        {
            this.contactInformations = new List<ContactInformationDTO>();
        }
    }


}