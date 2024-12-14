using AutoMapper;
using FamilyTree.BLL.DTO;
using FamilyTree.BLL.Exceptions;
using FamilyTree.DAL.Model;
using FamilyTree.DAL.Repository;
using FamilyTree.BLL.Exceptions;
using BLL.Exceptions;

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

        public async Task CreateMarriage(int firstSpouseId, int secondSpouseId)
        {
            if (firstSpouseId == secondSpouseId)
            {
                throw new SamePersonRelationshipException();
            }
            var firstSpouse = await _repository.GetPersonByIdAsync(firstSpouseId);
            if (firstSpouse == null)
            {
                throw new PersonDoesNotExistException(firstSpouseId);
            }
            var secondSpouse = await _repository.GetPersonByIdAsync(secondSpouseId);
            if (secondSpouse == null)
            {
                throw new PersonDoesNotExistException(secondSpouseId);
            }
            if (firstSpouse.SpouseId != null)
            {
                await ResetMarriage(firstSpouse);
            }
            if (secondSpouse.SpouseId != null)
            {
                await ResetMarriage(secondSpouse);
            }
            firstSpouse.SpouseId = secondSpouseId;
            secondSpouse.SpouseId = firstSpouseId;
            await _repository.UpdatePersonAsync(firstSpouse);
            await _repository.UpdatePersonAsync(secondSpouse);
        }

        public async Task DeleteMarriage(int Id)
        {
            var person = await _repository.GetPersonByIdAsync(Id);
            if (person == null)
            {
                throw new PersonDoesNotExistException(Id);
            }
            await ResetMarriage(person);
            
        }

        private async Task ResetMarriage(Person person)
        {
            if (person.SpouseId == null)
            {
                return;
            }
            else
            {
                var spouse = await _repository.GetPersonByIdAsync(person.SpouseId.Value);
                person.SpouseId = null;
                spouse.SpouseId = null;
                await _repository.UpdatePersonAsync(person);
                await _repository.UpdatePersonAsync(spouse);
            }

        }

        public async Task CreateParentChildRelationship(int childId, int parentId)
        {
            if (childId == parentId)
            {
                throw new SamePersonRelationshipException();
            }
            var child = await _repository.GetPersonByIdAsync(childId);
            if (child == null)
            {
                throw new PersonDoesNotExistException(childId);
            }
            var parent = await _repository.GetPersonByIdAsync(parentId);
            if (parent == null)
            {
                throw new PersonDoesNotExistException(parentId);
            }

        }

        public async Task CreatePerson(CreatePersonDTO person)
        {
            await _repository.AddPersonAsync(_mapper.Map<Person>(person));
        }

        public async Task DeleteParentChildRelationship(int childId, int parentId)
        {
            if (childId == parentId)
            {
                throw new SamePersonRelationshipException();
            }
            var child = await _repository.GetPersonByIdAsync(childId);
            if (child == null)
            {
                throw new PersonDoesNotExistException(childId);
            }
            var parent = await _repository.GetPersonByIdAsync(parentId);
            if (parent == null)
            {
                throw new PersonDoesNotExistException(parentId);
            }
            if (!child.Parents.Exists(p => p == parentId) || !parent.Children.Exists(c => c == childId) )
            {
                throw new NoRelationshipSetException(childId, parentId);
            }
            child.Parents.Remove(parentId);
            parent.Children.Remove(childId);
            await _repository.UpdatePersonAsync(child);
            await _repository.UpdatePersonAsync(parent);
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


        public async Task<List<PersonDTO>> GetChildrenById(int Id)
        {
            var person = await _repository.GetPersonByIdAsync(Id);
            if (person == null) {
                throw new PersonDoesNotExistException(Id);
            }
            List<PersonDTO> childrenData = [];
            foreach (var child in person.Children) {
                childrenData.Add(await GetPersonByIdAsync(child));
            }
            return childrenData;
        }

        public async Task<List<PersonDTO>> GetParentsById(int Id)
        {
            var person = await _repository.GetPersonByIdAsync(Id);
            if (person == null) {
                throw new PersonDoesNotExistException(Id);
            }
            List<PersonDTO> parentsData = [];
            foreach (var child in person.Parents)
            {
                parentsData.Add(await GetPersonByIdAsync(child));
            }
            return parentsData;
        }

        public async Task<PersonDTO> GetPersonByIdAsync(int Id)
        {
            var person = await _repository.GetPersonByIdAsync(Id);
            if (person == null)
            {
                throw new PersonDoesNotExistException(Id);
            }
            return _mapper.Map<PersonDTO>(person);
        }
    }
}
