using System;
using System.Threading.Tasks;
using Domain.AggregatesModel.ReportAggregate;
using Domain.AggregatesModel.StatisticAggregate;
using KafkaFlow;
using KafkaFlow.TypedHandler;

namespace API
{
    public class GenerateReportEventHandler : IMessageHandler<GenerateReportEvent>
    {
        private readonly IStatisticRepository statisticRepository;
        private readonly IReportRepository reportRepository;

        public GenerateReportEventHandler(IStatisticRepository statisticRepository, IReportRepository reportRepository)
        {
            this.statisticRepository = statisticRepository;
            this.reportRepository = reportRepository;
        }
        public async Task Handle(IMessageContext context, GenerateReportEvent message)
        {
            Console.WriteLine("GenerateReportEvent occured");

            var report = await reportRepository.FindAsync(message.identity);
            if (report != null)
            {
                Tuple<int,int> stats = statisticRepository.GetStats(message.location);
                report.SetReportStatistics(stats.Item1 ,stats.Item2);
                await reportRepository.UnitOfWork.SaveChangesAsync();
            }
            return;
        }
    }
}