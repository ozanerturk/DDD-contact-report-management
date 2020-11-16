using System.Runtime.Serialization;

namespace API
{
    [DataContract]
    public class AddContactInformationEvent
    {
        [DataMember]
        public int personId { get; set; }
        [DataMember]
        public string location { get; set; }
        [DataMember]
        public string phoneNumber { get; set; }
    }
}