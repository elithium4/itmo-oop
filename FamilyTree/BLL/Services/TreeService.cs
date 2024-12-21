


using AutoMapper;
using FamilyTree.BLL.Exceptions;
using FamilyTree.DAL.Model;
using FamilyTree.DAL.Repository;

namespace FamilyTree.BLL.Services
{
    public class TreeService : ITreeService
    {
        private readonly ITreeRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public TreeService(ITreeRepository treeRepository, IPersonRepository personRepository)
        {
            _repository = treeRepository;
            _personRepository = personRepository;
        }

        public async Task AddMemberAsync(int personId)
        {
            var trees = await _repository.GetAllTree();
            if (trees.Count == 0)
            {
                throw new NoTreeCreatedException();
            }
            var tree = trees[0];
            if (tree.Members.Contains(personId)) return;
            tree.Members.Add(personId);
            _repository.UpdateTree(tree);
        }

        public async Task CreateTree()
        {
            // Ограничение на одно дерево за раз
            var trees = await _repository.GetAllTree();
            if (trees.Count > 0)
            {
                throw new OnlyOneTreeAllowedException();
            }
            await _repository.CreateTree();
        }

        public async Task DeleteMemberAsync(int personId)
        {
            var trees = await _repository.GetAllTree();
            if (trees.Count == 0)
            {
                throw new NoTreeCreatedException();
            }
            var tree = trees[0];
            if (!tree.Members.Contains(personId)) return;
            tree.Members.Remove(personId);
            _repository.UpdateTree(tree);
        }

        public async Task<Tree> GetOrCreateTree()
        {
            var trees = await _repository.GetAllTree();
            if (trees.Count == 0)
            {
                await _repository.CreateTree();
                trees = await _repository.GetAllTree();
            }
            return trees[0];
        }

        public async Task<List<Person>> GetMembersAsync()
        {
            var tree = await GetOrCreateTree();
            List<Person> members = new List<Person>();
            foreach (var item in tree.Members)
            {
                var memberData = await  _personRepository.GetPersonByIdAsync(item);
                members.Add(memberData);
            }
            return members;
        }

        public async Task EmptyTree()
        {
            var tree = await GetOrCreateTree();
            await _repository.DeleteTree(tree);
        }
    }
}
