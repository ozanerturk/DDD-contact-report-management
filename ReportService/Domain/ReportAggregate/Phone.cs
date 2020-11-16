using Domain.Bases;
using System;
using System.Collections.Generic;

namespace Domain.AggregatesModel.PersonAggregate
{
    public class Phone : ValueObject
    {
        public string PhoneNumber { get; private set; }

        public Phone() { }

        public Phone(string phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return PhoneNumber;
          
        }
    }
}
