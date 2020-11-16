using Domain.AggregatesModel.PersonAggregate;
using Domain.AggregatesModel.StatisticAggregate;
using Domain.Bases;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StatisticRepository
        : IStatisticRepository
    {
        private readonly ReportContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public StatisticRepository(ReportContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Statistic Add(Statistic s)
        {
            if (s.IsTransient())
            {
                return _context.Statistics
                    .Add(s)
                    .Entity;
            }
            else
            {
                return s;
            }
        }


        public Statistic Delete(Statistic p)
        {
              return _context.Statistics.Remove(p).Entity;
        }

        public void DeleteRange(List<Statistic> p)
        {
          _context.Statistics.RemoveRange(p);
        }

        public Task<Statistic> FindAsync(int personId, string phoneNumber)
        {
          return _context.Statistics.Where(x=>x.PersonId== personId && x.PhoneNumber == phoneNumber).SingleOrDefaultAsync();
        }
        
        public Task<List<Statistic>> FindAsync(int personId)
        {
            return _context.Statistics.Where(x=>x.PersonId== personId).ToListAsync();
        }
    }
}
