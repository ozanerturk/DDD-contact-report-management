namespace API.Controllers
{
    public class PersonDTO
    {
        public int Id {get;set;}
          public string IdentityGuid { get;  set; }

        public string Name { get;  set; }

        public string Surname { get;  set; }
        public string Company { get;  set; }
    }
}