using System.Runtime.Serialization;

namespace API
{
    [DataContract]
    public class RemoveContactInformationEvent
    {
        [DataMember]
        public int personId { get; set; }
        [DataMember]
        public string phoneNumber { get; set; }
    }
}