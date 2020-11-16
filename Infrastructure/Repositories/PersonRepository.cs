using Domain.AggregatesModel.PersonAggregate;
using Domain.Bases;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PersonRepository
        : IPersonRepository
    {
        private readonly PersonContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public PersonRepository(PersonContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Person Add(Person person)
        {
            if (person.IsTransient())
            {
                return _context.Persons
                    .Add(person)
                    .Entity;
            }
            else
            {
                return person;
            }           
        }

        public Person Update(Person person)
        {
            return _context.Persons
                    .Update(person)
                    .Entity;
        }

        public async Task<Person> FindAsync(string identity)
        {
            var person = await _context.Persons
                .Include(b => b.ContactInformations)
                .Where(b => b.IdentityGuid == identity)
                .SingleOrDefaultAsync();

            return person;
        }

        public async Task<Person> FindByIdAsync(string id)
        {
            var person = await _context.Persons
                .Include(b => b.ContactInformations)
                .Where(b => b.Id == int.Parse(id))
                .SingleOrDefaultAsync();

            return person;
        }

        public async Task<List<Person>> Get()
        {
            return  await _context.Persons.ToListAsync();
        }
    }
}
