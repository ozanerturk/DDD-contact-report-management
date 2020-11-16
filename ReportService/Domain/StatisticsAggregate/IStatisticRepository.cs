using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Bases;

namespace Domain.AggregatesModel.StatisticAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Statistic Aggregate

    public interface IStatisticRepository : IRepository<Statistic>
    {
        Statistic Add(Statistic Statistic);
        Task<List<Statistic>> FindAsync(int personıd);
        Statistic Delete(Statistic p);
        void DeleteRange(List<Statistic> p);
        Task<Statistic> FindAsync(int personId, string phoneNumber);
        Tuple<int,int> GetStats(string location);
    }
}
