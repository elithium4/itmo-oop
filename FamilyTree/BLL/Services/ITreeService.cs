using FamilyTree.DAL.Model;

namespace FamilyTree.BLL.Services
{
    public interface ITreeService
    {
        public Task CreateTree();
        public Task<Tree> GetOrCreateTree();
        public Task AddMemberAsync(int Id);
        public Task DeleteMemberAsync(int Id);
        public Task<List<Person>> GetMembersAsync();

        public Task EmptyTree();
    }
}
