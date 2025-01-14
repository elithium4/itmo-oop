using FamilyTree.DAL.Model;

namespace FamilyTree.DAL.Repository
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllPeopleAsync();
        Task<Person> GetPersonByIdAsync(int id);
        Task AddPersonAsync(Person person);
        Task RemovePersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task AddParentChildRelationshipAsync(int childId, int parentId);
        Task RemoveParentChildRelationshipAsync(int childId, int parentId);

    }
}
