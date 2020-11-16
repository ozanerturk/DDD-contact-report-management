using Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.AggregatesModel.StatisticAggregate
{
    public class Statistic
      : Entity, IAggregateRoot
    {
        public string Location { get; private set; }
        public int PersonId { get; private set; }
        public string PhoneNumber { get; private set; }

        protected Statistic()
        {
        }

        public Statistic(string location, int personId, string phoneNumber) : this()
        {
            this.Location = !string.IsNullOrWhiteSpace(location) ? location : throw new ArgumentNullException(nameof(location));
            this.PhoneNumber = !string.IsNullOrWhiteSpace(phoneNumber) ? phoneNumber : throw new ArgumentNullException(nameof(phoneNumber));
            this.PersonId = personId;
        }

    }
}
