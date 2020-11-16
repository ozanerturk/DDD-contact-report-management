using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AggregatesModel.ReportAggregate;
using System;
using Infrastructure;

namespace Infrastructure.EntityConfigurations
{
    class ReportEntityTypeConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> statisticConfiguration)
        {
            statisticConfiguration.ToTable("reports", ReportContext.DEFAULT_SCHEMA);

            statisticConfiguration.HasKey(o => o.Id);

            statisticConfiguration.Property(o => o.Id)
                .UseHiLo("reportsseq", ReportContext.DEFAULT_SCHEMA);

            statisticConfiguration.Property(x => x.IdentityGuid);
            statisticConfiguration.Property(x => x.TotalDistinctPersons);
            statisticConfiguration.Property(x => x.TotalDistinctPhoneNumbers);
            statisticConfiguration.Property(x => x.Status);
    


        }
    }
}
