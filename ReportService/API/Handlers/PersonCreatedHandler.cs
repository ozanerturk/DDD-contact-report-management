using System.Threading.Tasks;
using KafkaFlow;
using KafkaFlow.TypedHandler;

namespace API
{
    public class PersonCreatedHandler : IMessageHandler<PersonCreatedEvent>
    {
        public PersonCreatedHandler()
        {
            
        }
        public Task Handle(IMessageContext context, PersonCreatedEvent message)
        {
            throw new System.NotImplementedException();
        }
    }
}