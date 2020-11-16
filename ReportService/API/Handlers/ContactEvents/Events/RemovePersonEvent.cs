using System.Runtime.Serialization;

namespace API
{
    [DataContract]
    public class RemovePersonEvent
    {
        [DataMember]
        public int personId { get; set; }
    }
}