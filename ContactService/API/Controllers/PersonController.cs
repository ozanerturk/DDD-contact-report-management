using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Domain.AggregatesModel.PersonAggregate;
using KafkaFlow.Producers;
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
        private readonly IProducerAccessor producerAccessor;

        public PersonController(ILogger<PersonController> logger, IPersonRepository personRepository,IProducerAccessor producerAccessor)
        {
            _logger = logger;
            this.personRepository = personRepository;
            this.producerAccessor = producerAccessor;
        }


        [Route("{personId:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete]
        public async Task<ActionResult> DeletePerson(int personId)
        {

            Person p = await personRepository.FindByIdAsync(personId);
            if (p == null)
            {
                return NotFound();
            }
            personRepository.Delete(p);

            await personRepository.UnitOfWork.SaveChangesAsync();
            return Ok();
        }

        [Route("{personId:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        public async Task<ActionResult<PersonDetailedDTO>> GetPerson(int personId)
        {

            Person p = await personRepository.FindByIdAsync(personId);
            if (p == null)
            {
                return NotFound();
            }

            var result = new PersonDetailedDTO()
            {
                company = p.Company,
                id = p.Id,
                identityGuid = p.IdentityGuid,
                name = p.Name,
                surname = p.Surname,
                contactInformations = p.ContactInformations.Select(x => new ContactInformationDTO()
                {
                    id = x.Id,
                    description = x.Desciption,
                    emailAddress = x.Email.EmailAddress,
                    location = x.Location,
                    phoneNumber = x.Phone.PhoneNumber
                }).ToList()
            };

            return Ok(result);
        }


        [Route("{personId:int}/contact-information/{contactInformationId:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete]
        public async Task<ActionResult> DeleteContactInformation(int personId, int contactInformationId)
        {

            Person p = await personRepository.FindByIdAsync(personId);
            if (p == null)
            {
                return NotFound();
            }

            p.RemoveContactInformation(contactInformationId);

            await personRepository.UnitOfWork.SaveChangesAsync();

            return Ok();
        }

        [Route("{personId:int}/contact-information")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<ActionResult<ContactInformationDTO>> AddContactInformation(int personId, [FromBody] CreateContactInformationRequest request)
        {

            Person p = await personRepository.FindByIdAsync(personId);
            if (p == null)
            {
                return NotFound();
            }

            Phone phone = new Phone(request.phoneNumber);
            Email email = Email.Create(request.emailAddress);


            ContactInformation c = p.AddContactInformation(phone, email, request.location, request.description);

            await personRepository.UnitOfWork.SaveChangesAsync();

            return Ok(new ContactInformationDTO()
            {
                description = c.Desciption,
                emailAddress = c.Email.EmailAddress,
                location = c.Location,
                phoneNumber = c.Phone.PhoneNumber
            });
        }


        [HttpPost]
        public async Task<ActionResult<PersonDTO>> PostUser([FromBody] CreatePersonRequest request)
        {
            Person p = new Person(Guid.NewGuid().ToString(), request.name, request.surname, request.company);
            p = personRepository.Add(p);

            await personRepository.UnitOfWork.SaveChangesAsync();

            
            await producerAccessor.All.First().ProduceAsync(Guid.NewGuid().ToString(),new CreatePersonEvent(){personId = p.Id});
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