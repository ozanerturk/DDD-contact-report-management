using System;
using System.Threading.Tasks;
using Domain.AggregatesModel.StatisticAggregate;
using KafkaFlow;
using KafkaFlow.TypedHandler;

namespace API
{
    public class RemoveContactInformationEventHandler : IMessageHandler<RemoveContactInformationEvent>
    {
        private readonly IStatisticRepository statisticRepository;

        public RemoveContactInformationEventHandler(IStatisticRepository statisticRepository)
        {
            this.statisticRepository = statisticRepository;
        }
        public async Task Handle(IMessageContext context, RemoveContactInformationEvent message)
        {

            var stat =await statisticRepository.FindAsync(message.personId,message.phoneNumber);
            if(stat != null){
                statisticRepository.Delete(stat);
            }
            await statisticRepository.UnitOfWork.SaveChangesAsync();

            return;
        }
    }
}