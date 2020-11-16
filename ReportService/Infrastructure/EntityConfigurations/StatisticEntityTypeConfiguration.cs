using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AggregatesModel.StatisticAggregate;
using System;
using Infrastructure;

namespace Infrastructure.EntityConfigurations
{
    class StatisticEntityTypeConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> statisticConfiguration)
        {
            statisticConfiguration.ToTable("statistics", ReportContext.DEFAULT_SCHEMA);

            statisticConfiguration.HasKey(o => o.Id);

            // statisticConfiguration.Ignore(b => b.DomainEvents);

            statisticConfiguration.Property(o => o.Id)
                .UseHiLo("statisticseq", ReportContext.DEFAULT_SCHEMA);

            statisticConfiguration.Property(x => x.Location);
            statisticConfiguration.Property(x => x.PersonId);
            statisticConfiguration.Property(x => x.PhoneNumber);

        }
    }
}
