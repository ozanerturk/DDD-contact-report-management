using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infrastructure;

namespace API.Factories
{
    public class OrderingDbContextFactory : IDesignTimeDbContextFactory<PersonContext>
    {
        public PersonContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PersonContext>();

            optionsBuilder.UseNpgsql(config["ConnectionString"], npgsqlOptionsAction : o => o.MigrationsAssembly("API"));

            return new PersonContext(optionsBuilder.Options);
        }
    }
}