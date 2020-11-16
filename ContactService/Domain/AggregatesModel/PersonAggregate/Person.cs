using Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.AggregatesModel.PersonAggregate
{
    public class Person
      : Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }
        public string Company { get; private set; }

        private List<ContactInformation> _contactInformations;

        public IEnumerable<ContactInformation> ContactInformations => _contactInformations.AsReadOnly();

        protected Person()
        {

            _contactInformations = new List<ContactInformation>();
        }

        public Person(string identity, string name, string surname, string company) : this()
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            Surname = !string.IsNullOrWhiteSpace(surname) ? surname : throw new ArgumentNullException(nameof(surname));
            Company = !string.IsNullOrWhiteSpace(company) ? company : throw new ArgumentNullException(nameof(company));
        }

        public int GetContactInformationCount()
        {
            return this.ContactInformations.Count();
        }

        public ContactInformation RemoveContactInformation(int contactInformationId)
        {
            var existsContactInformation = this._contactInformations.SingleOrDefault(x => contactInformationId == x.Id);
            if(existsContactInformation==null){
                throw new ContactInformationNotFoundException();
            }
            this._contactInformations.Remove(existsContactInformation);
            return existsContactInformation;

        }

        public ContactInformation AddContactInformation(
            Phone phoneNumber, Email email, string location, string description)
        {
            var existingContactInformation = _contactInformations
                .SingleOrDefault(c => c.IsEqualTo(phoneNumber));

            if (existingContactInformation != null)
            {
                throw new ContactInformationAlreadyExistsException();
            }

            var contactInformation = new ContactInformation(phoneNumber, email, location, description);

            _contactInformations.Add(contactInformation);

            // AddDomainEvent(new PersonAndContactInformationMethodVerifiedDomainEvent(this, payment, orderId));
            return contactInformation;

        }
    }
}
