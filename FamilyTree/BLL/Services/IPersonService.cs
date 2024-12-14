using FamilyTree.BLL.DTO;

namespace FamilyTree.BLL.Services
{
    public interface IPersonService
    {
        public Task<List<PersonDTO>> GetAllPeopleAsync();
        public Task<PersonDTO> GetPersonByIdAsync(int id);
        public Task CreatePerson(CreatePersonDTO person);
        public Task CreateParentChildRelationship(int childId, int parentId);
        public Task DeleteParentChildRelationship(int childId, int parentId);
        public Task CreateMarriage(int firstSpouseId, int secondSpouseId);
        public Task DeleteMarriage(int personId);
        public Task<int> CalculateAncestorAgeAtBirth(int childId, int ancestorId);
        public Task<List<PersonDTO>> GetParentsById(int Id);
        public Task<List<PersonDTO>> GetChildrenById(int Id);
        public Task<List<PersonDTO>> FindCommonRelatives(int firstPersonId, int secondPersonId);

    }
}
