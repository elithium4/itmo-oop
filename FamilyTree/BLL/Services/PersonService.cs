using AutoMapper;
using FamilyTree.BLL.DTO;
using FamilyTree.DAL.Model;
using FamilyTree.DAL.Repository;

namespace FamilyTree.BLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;
        public PersonService(IPersonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CalculateAgeAtBirth(int childId, int ancestorId)
        {
            throw new NotImplementedException();
        }

        public Task CreateMarriage(int firstSpouseId, int secondSpouseId)
        {
            throw new NotImplementedException();
        }

        public Task CreateParentChildRelationship(int childId, int parentId)
        {
            throw new NotImplementedException();
        }

        public async Task CreatePerson(CreatePersonDTO person)
        {
            await _repository.AddPersonAsync(_mapper.Map<Person>(person));
        }

        public Task DeleteMarriage(int firstSpouseId, int secondSpouseId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteParentChildRelationship(int childId, int parentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonDTO>> FindCommonRelatives(int firstPersonId, int secondPersonId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PersonDTO>> GetAllPeopleAsync()
        {
            var people = await _repository.GetAllPeopleAsync();
            return _mapper.Map<List<PersonDTO>>(people);
        }


        public Task<List<PersonDTO>> GetChildrenById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonDTO>> GetParentsById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonDTO> GetPersonByIdAsync(int Id)
        {
            var person = await _repository.GetPersonByIdAsync(Id);
            if (person == null)
            {
            }
            return _mapper.Map<PersonDTO>(person);
        }
    }
}
