using System;
using System.Collections.Generic;

namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.Queries
{
    public class Report
    {
        public DateTime date { get; set; }
        public string status { get; set; }
    }

    public class ReportItem
    {
        public string location { get; set; }
        public int distinctPhoneNumberCount { get; set; }
        public int distinctPersonCount { get; set; }
    }

}
