using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.AggregatesModel.PersonAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository personRepository;

        public PersonController(ILogger<PersonController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            this.personRepository = personRepository;
        }

        [HttpPost]
        public async Task<ActionResult<PersonDTO>> PostUser([FromBody] CreatePersonRequest request)
        {
            var rng = new Random();
            Person p = new Person(Guid.NewGuid().ToString(), "ozan", "ertürk", "sensemore");
            p = personRepository.Add(p);

            await personRepository.UnitOfWork.SaveChangesAsync();
            return Ok(p);
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            List<Person> persons = await personRepository.Get();
            var dtoList = persons.Select(x =>
                new PersonDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    IdentityGuid = x.IdentityGuid

                });

            return Ok(dtoList);
        }
    }
}
