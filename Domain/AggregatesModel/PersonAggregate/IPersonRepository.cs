using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Bases;

namespace Domain.AggregatesModel.PersonAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Person Aggregate

    public interface IPersonRepository : IRepository<Person>
    {
        Person Add(Person person);
        Person Update(Person person);
        Task<Person> FindAsync(string PersonIdentityGuid);
        Task<List<Person>> Get();
        Task<Person> FindByIdAsync(string id);
    }
}
