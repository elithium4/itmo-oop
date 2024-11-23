using Lab3.Repositories;
using Lab3.Repositories.Model;
using System.IO;

namespace Lab3.Repositories.File
{
    public class FileStoreRepository : BaseFileRepository, IStoreRepository
    {
        private Random _idGenerator = new Random();
        public FileStoreRepository(string filePath) : base(filePath) { }

        public async Task CreateStoreAsync(Store store)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath);
            var newLine = $"{_idGenerator.Next(1, 100000000)},{store.Name},{store.Address}";
            await System.IO.File.AppendAllLinesAsync(_filePath, [newLine]);
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath);
            return lines.Select(line =>
            {
                var parts = line.Split(',');
                return new Store
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Address = parts[2]
                };
            }).ToList();
        }

        public async Task<Store> GetStoreByIdAsync(int id)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath);
            var line = lines.FirstOrDefault(l => l.StartsWith($"{id},"));
            if (line == null) return null;

            var parts = line.Split(',');
            return new Store
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Address = parts[2]
            };
        }
    }
}
