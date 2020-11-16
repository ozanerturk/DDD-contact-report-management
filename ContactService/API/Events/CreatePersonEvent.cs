using System.Runtime.Serialization;

namespace API
{
    [DataContract]
    public class CreatePersonEvent
    {
        [DataMember(Order = 1)]
        public int personId { get; set; }
    }
}