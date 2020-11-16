using System.Runtime.Serialization;

namespace API
{
    [DataContract]
    public class GenerateReportEvent
    {
        [DataMember]
        public string location { get; set; }
        [DataMember]
        public string identity { get; set; }
    }
}