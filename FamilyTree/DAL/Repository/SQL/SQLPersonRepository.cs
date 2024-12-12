using FamilyTree.DAL.Model;
using FamilyTree.DAL.Repositories.SQL;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.DAL.Repository.SQL
{
    public class SQLPersonRepository : IPersonRepository
    {
        private readonly ApplicationContext _context;

        public SQLPersonRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int Id)
        {
            return await _context.People.FindAsync(Id);
        }

        public async Task AddPersonAsync(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            var existingPerson = _context.People.Find(person.Id);
            if (existingPerson == null)
            {
                _context.People.Add(person);
            }
            else
            {
                _context.People.Update(person);
            }
        }

        public async Task RemovePersonAsync(Person person)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task AddParentChildRelationshipAsync(int childId, int parentId)
        {
            _context.People.Find(childId).Parents.Add(parentId);
            _context.People.Find(parentId).Children.Add(childId);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveParentChildRelationshipAsync(int childId, int parentId)
        {
            _context.People.Find(childId).Parents.Remove(parentId);
            _context.People.Find(parentId).Children.Remove(childId);
            await _context.SaveChangesAsync();
        }
    }
}
