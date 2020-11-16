using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AggregatesModel.PersonAggregate;
using System;
using Infrastructure;

namespace Ordering.Infrastructure.EntityConfigurations
{
    class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> personConfiguration)
        {
            personConfiguration.ToTable("persons", PersonContext.DEFAULT_SCHEMA);

            personConfiguration.HasKey(o => o.Id);

            // personConfiguration.Ignore(b => b.DomainEvents);

            personConfiguration.Property(o => o.Id)
                .UseHiLo("personseq", PersonContext.DEFAULT_SCHEMA);

            //Address value object persisted as owned entity type supported since EF Core 2.0
            personConfiguration.Property(b => b.IdentityGuid)
                       .HasMaxLength(200)
                       .IsRequired();
            personConfiguration.Property(x => x.Name);
            personConfiguration.Property(x => x.Surname);
            personConfiguration.HasMany(x => x.ContactInformations);

        }
    }
}
