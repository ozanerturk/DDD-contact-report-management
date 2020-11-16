using Domain.Bases;
using System;

namespace Domain.AggregatesModel.PersonAggregate
{
    public class ContactInformation
        : Entity
    {
        public Phone Phone {get; private set;}
        public Email Email {get; private set;}
        public  string Desciption {get; private set;}
        public string Location {get; private set;}

        protected ContactInformation() { }

        public ContactInformation(Phone phone, Email email, string location, string desciption)
        {

            this.Desciption = !string.IsNullOrWhiteSpace(desciption) ? desciption : throw new ArgumentNullException(nameof(desciption));
            this.Location = !string.IsNullOrWhiteSpace(location) ? location : throw new ArgumentNullException(nameof(location));
            this.Phone =phone;
            this.Email =email;
        }

        public bool IsEqualTo(Phone phone)
        {
            return this.Phone.Equals(phone);
        }

        internal ContactInformation Update(Phone phone, Email email, string location, string description)
        {
            this.Desciption = !string.IsNullOrWhiteSpace(Desciption) ? Desciption : throw new ArgumentNullException(nameof(Desciption));
            this.Location = !string.IsNullOrWhiteSpace(location) ? location : throw new ArgumentNullException(nameof(location));
            this.Phone =phone;
            this.Email =email;
            return this;
        }
    }
}
