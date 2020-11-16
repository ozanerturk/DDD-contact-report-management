using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Bases;

namespace Domain.AggregatesModel.ReportAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Report Aggregate

    public interface IReportRepository : IRepository<Report>
    {
        Report Add(Report report);
        Task<Report> FindAsync(string identity);
    }
}
