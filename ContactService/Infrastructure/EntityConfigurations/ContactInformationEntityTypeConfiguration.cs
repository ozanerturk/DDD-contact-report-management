using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AggregatesModel.PersonAggregate;
using System;
using Infrastructure;

namespace Ordering.Infrastructure.EntityConfigurations
{
    class ContactInformationEntityTypeConfiguration : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> contactInformationConfiguration)
        {
            contactInformationConfiguration.ToTable("contactInformations", PersonContext.DEFAULT_SCHEMA);

            contactInformationConfiguration.HasKey(o => o.Id);

            contactInformationConfiguration.Property<int>("PersonId")
            .IsRequired();

            contactInformationConfiguration.OwnsOne(x => x.Phone, a =>
            {
                a.Property(p => p.PhoneNumber).HasColumnName("PhoneNumber");
            });

            contactInformationConfiguration.OwnsOne(x => x.Email, a =>
            {
                a.Property(p => p.EmailAddress).HasColumnName("Email");
            });
            contactInformationConfiguration.Property(x => x.Desciption);
            contactInformationConfiguration.Property(x => x.Location);

        }
    }
}
