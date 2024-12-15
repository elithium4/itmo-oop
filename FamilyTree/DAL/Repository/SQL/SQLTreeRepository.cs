using FamilyTree.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace FamilyTree.DAL.Repository.SQL
{
    public class SQLTreeRepository: ITreeRepository
    {
        private readonly ApplicationContext _context;
        public SQLTreeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateTree()
        {
            _context.Tree.Add(new Tree{ Members = new List<int>()});
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTree(Tree tree)
        {
            var existingTree = _context.Tree.Find(tree.Id);
            if (existingTree == null)
            {
                _context.Tree.Add(tree);
            }
            else
            {
                _context.Tree.Update(tree);
            }
            await _context.SaveChangesAsync();
        }

        public Task EmptyTree(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tree> GetTreeById(int id)
        {
            return await _context.Tree.FindAsync(id);
        }

        public async Task<List<Tree>> GetAllTree()
        {
            return await _context.Tree.ToListAsync();
        }

    }
}
