
using FamilyTree.DAL.Model;

namespace FamilyTree.DAL.Repository
{
    public interface ITreeRepository
    {
        Task CreateTree();
        Task UpdateTree(Tree tree);
        Task EmptyTree(int id);
        Task<Tree> GetTreeById(int id);
        Task <List<Tree>> GetAllTree();
    }
}
