using Domain.Bases;
using System;

namespace Domain.AggregatesModel.PersonAggregate
{
    public class ContactInformation
        : Entity
    {
        private Phone phone;
        private Email email;
        private string desciption;
        private string location;

        protected ContactInformation() { }

        public ContactInformation(Phone phone, Email email, string desciption, string location)
        {

            this.desciption = !string.IsNullOrWhiteSpace(desciption) ? desciption : throw new ArgumentNullException(nameof(desciption));
            this.location = !string.IsNullOrWhiteSpace(location) ? location : throw new ArgumentNullException(nameof(location));
            this.phone =phone;
            this.email =email;
        }

        public bool IsEqualTo(Phone phone)
        {
            return this.phone.Equals(phone);
        }
    }
}
