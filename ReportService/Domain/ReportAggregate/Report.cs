using Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.AggregatesModel.ReportAggregate
{
    public class Report
      : Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }
        public string Location { get; private set; }
        public int TotalDistinctPhoneNumbers { get; private set; }
        public int TotalDistinctPersons { get; private set; }
        public Status Status { get; private set; }
        public DateTime RequestDate { get; private set; }


        protected Report()
        {

        }

        public Report(string identity,string location) : this()
        {
            Location = !string.IsNullOrWhiteSpace(location) ? location : throw new ArgumentNullException(nameof(location));
            IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
            RequestDate = DateTime.Now;
            Status = Status.PENDING;
        }

        public void SetReportStatistics(int totalDistinctPersons,int totalPhoneNumbers){
            this.TotalDistinctPersons = totalDistinctPersons;
            this.TotalDistinctPhoneNumbers= totalPhoneNumbers;
            this.Status = Status.READY;
        }
      
    }
}
