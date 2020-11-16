using Domain.AggregatesModel.ReportAggregate;
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
    public class ReportRepository
        : IReportRepository
    {
        private readonly ReportContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ReportRepository(ReportContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public Report Add(Report s)
        {
            if (s.IsTransient())
            {
                return _context.Reports
                    .Add(s)
                    .Entity;
            }
            else
            {
                return s;
            }
        }

        public Task<List<Report>> Get()
        {
            return _context.Reports.ToListAsync();
        }

        public Task<Report> FindAsync(string identity)
        {
            return _context.Reports.SingleOrDefaultAsync(x => x.IdentityGuid == identity);
        }
    }
}
