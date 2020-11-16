using System;
using System.Threading.Tasks;
using Domain.AggregatesModel.StatisticAggregate;
using KafkaFlow;
using KafkaFlow.TypedHandler;

namespace API
{
    public class AddContactInformationEventHandler : IMessageHandler<AddContactInformationEvent>
    {
        private readonly IStatisticRepository statisticRepository;

        public AddContactInformationEventHandler(IStatisticRepository statisticRepository)
        {
            this.statisticRepository = statisticRepository;
        }
        public async Task Handle(IMessageContext context, AddContactInformationEvent message)
        {

            statisticRepository.Add(new Statistic(personId:message.personId,location:message.location,phoneNumber:message.phoneNumber));
            await statisticRepository.UnitOfWork.SaveChangesAsync();

            return;
        }
    }
}