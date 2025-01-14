using AutoMapper;
using FamilyTree.BLL.DTO;
using FamilyTree.BLL.Exceptions;
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

        public async Task<int> CalculateAncestorAgeAtBirth(int childId, int ancestorId)
        {
            var child = await GetPersonByIdAsync(childId);
            if (child == null)
            {
                throw new PersonDoesNotExistException(childId);
            }
            var ancestor = await GetPersonByIdAsync(ancestorId);
            if (child == null)
            {
                throw new PersonDoesNotExistException(ancestorId);
            }
            if (child.Birthdate < ancestor.Birthdate)
            {
                throw new BadAgeDifferenceException();
            }
            int pureYearsDifference = child.Birthdate.Year - ancestor.Birthdate.Year;
            if (child.Birthdate < ancestor.Birthdate.AddYears(pureYearsDifference))
            {
                pureYearsDifference--;
            }
            return pureYearsDifference;
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
            var canMarry = await CanMarry(firstSpouseId, secondSpouseId);
            if (!canMarry)
            {
                throw new IllegalMarriageCandidateException();
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

        private async Task<bool> CanMarry(int firstSpouseId, int secondSpouseId)
        {
            var relatives = await GetAllRelativesId(firstSpouseId);
            if (relatives.Contains(secondSpouseId))
            {
                return false;
            }
            return true;
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
            if (child.Parents.Count >= 2)
            {
                throw new IllegalParentsCountException();
            }
            if (parent.Birthdate >= child.Birthdate)
            {
                throw new IllegalAgeDifferenceException();
            }
            if (parent.Children.Contains(childId))
            {
                return;
            }
            var canHaveRelationship = await CanHaveParentChildRelationship(parent, child);
            if (!canHaveRelationship)
            {
                throw new IllegalParentChildRelationshipException();
            }
            child.Parents.Add(parentId);
            parent.Children.Add(childId);
            await _repository.UpdatePersonAsync(child);
            await _repository.UpdatePersonAsync(parent);
        }

        private async Task<bool> CanHaveParentChildRelationship(Person parent, Person child)
        {
            if (parent.SpouseId == child.Id || child.SpouseId == parent.Id)
            {
                return false;
            }
            var relatives = await GetAllRelativesId(parent.Id);
            if (relatives.Contains(child.Id))
            {
                return false;
            }

            return true;
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

        public async Task<List<PersonDTO>> FindCommonRelatives(int firstPersonId, int secondPersonId)
        {
            HashSet<int> checkedFirstRelatives = new HashSet<int>();
            HashSet<int> checkedSecondRelatives = new HashSet<int>();

            Queue<int> firstRelativesQueue = new Queue<int>();
            Queue<int> secondRelativesQueue = new Queue<int>();

            firstRelativesQueue.Enqueue(firstPersonId);
            secondRelativesQueue.Enqueue(secondPersonId);
            List<PersonDTO> commonRelativesPair = new List<PersonDTO>();

            while (firstRelativesQueue.Count > 0 || secondRelativesQueue.Count > 0)
            {
                if (firstRelativesQueue.Count > 0)
                {
                    int currentFirst = firstRelativesQueue.Dequeue();
                    if (checkedSecondRelatives.Contains(currentFirst))
                    {
                        int firstParentId = currentFirst;
                        var firstParent = await GetPersonByIdAsync(currentFirst);
                        if (firstParent == null)
                        {
                            throw new PersonDoesNotExistException(currentFirst);
                        }
                        commonRelativesPair.Add(firstParent);
                        if (firstParentId != secondPersonId && firstParent.SpouseId.HasValue)
                        {
                            var secondParent = await GetPersonByIdAsync(firstParent.SpouseId.Value);
                            commonRelativesPair.Add(secondParent);
                        }
                        return commonRelativesPair;
                    }

                    checkedFirstRelatives.Add(currentFirst);
                    var ancestors = await GetParentsById(currentFirst);
                    foreach (var item in ancestors)
                    {
                        if (!checkedFirstRelatives.Contains(item.Id)) ;
                        {
                            firstRelativesQueue.Enqueue(item.Id);
                        }
                    }
                }

                if (secondRelativesQueue.Count > 0)
                {
                    int currentSecond = secondRelativesQueue.Dequeue();
                    if (checkedFirstRelatives.Contains(currentSecond))
                    {
                        int firstParentId = currentSecond;
                        var firstParent = await GetPersonByIdAsync(currentSecond);
                        if (firstParent == null)
                        {
                            throw new PersonDoesNotExistException(currentSecond);
                        }
                        commonRelativesPair.Add(firstParent);
                        if (firstParentId != firstPersonId && firstParent.SpouseId.HasValue)
                        {
                            var secondParent = await GetPersonByIdAsync(firstParent.SpouseId.Value);
                            commonRelativesPair.Add(secondParent);
                        }
                        return commonRelativesPair;
                    }

                    checkedSecondRelatives.Add(currentSecond);
                    var ancestors = await GetParentsById(currentSecond);
                    foreach (var item in ancestors)
                    {
                        if (!checkedSecondRelatives.Contains(item.Id)) ;
                        {
                            secondRelativesQueue.Enqueue(item.Id);
                        }
                    }
                }
            }
            throw new NoCommonRelativesException(firstPersonId, secondPersonId);
        }

        private async Task<HashSet<int>> GetAllRelativesId(int personId)
        {
            HashSet<int> relatives = new HashSet<int>();
            var checkQueue = new Queue<int>();
            checkQueue.Enqueue(personId);
            while (checkQueue.Count > 0)
            {
                int id = checkQueue.Dequeue();
                var person = await _repository.GetPersonByIdAsync(id);
                if (person == null)
                {
                    continue;
                }
                person.Parents.ForEach(p =>
                {
                    if (relatives.Add(p))
                    {
                        checkQueue.Enqueue(p);
                    }
                });
                person.Children.ForEach(p =>
                {
                    if (relatives.Add(p))
                    {
                        checkQueue.Enqueue(p);
                    }
                });
            }
            return relatives;

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
            foreach (var item in person.Children)
            {
                var childData = _mapper.Map < PersonDTO > (await _repository.GetPersonByIdAsync(item));
                childrenData.Add(childData);
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
            foreach (var item in person.Parents)
            {
                var parentData = _mapper.Map<PersonDTO>(await _repository.GetPersonByIdAsync(item));
                parentsData.Add(parentData);
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

        public async Task<PersonDTO> GetPersonSpouseAsync(int Id)
        {
            var person = await _repository.GetPersonByIdAsync(Id);
            if (person == null)
            {
                throw new PersonDoesNotExistException(Id);
            }
            if (!person.SpouseId.HasValue)
            {
                return null;
            }
            var spouse = await _repository.GetPersonByIdAsync(person.SpouseId.Value);
            if (spouse == null)
            {
                throw new PersonDoesNotExistException(person.SpouseId.Value);
            }
            return _mapper.Map<PersonDTO>(spouse);
        }
    }
}
