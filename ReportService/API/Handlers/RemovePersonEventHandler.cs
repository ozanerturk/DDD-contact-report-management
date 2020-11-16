using System;
using System.Threading.Tasks;
using Domain.AggregatesModel.StatisticAggregate;
using KafkaFlow;
using KafkaFlow.TypedHandler;

namespace API
{
    public class RemovePersonEventHandler : IMessageHandler<RemovePersonEvent>
    {
        private readonly IStatisticRepository statisticRepository;

        public RemovePersonEventHandler(IStatisticRepository statisticRepository)
        {
            this.statisticRepository = statisticRepository;
        }
        public async Task Handle(IMessageContext context, RemovePersonEvent message)
        {
            Console.WriteLine("RemovePersonEvent occured");
            
            var stats = await statisticRepository.FindAsync(message.personId);

            statisticRepository.DeleteRange(stats);

            await statisticRepository.UnitOfWork.SaveChangesAsync();

            return;
        }
    }
}