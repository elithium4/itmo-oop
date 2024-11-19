using Lab3.Model;
using Lab3.Reposiories;

namespace Lab3.Repositories
{
    public class FileStoreRepository: IStoreRepository
    {
        private readonly string _filePath;

        public FileStoreRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task CreateStoreAsync(Store store)
        {
            var lines = await File.ReadAllLinesAsync(_filePath);
            var newLine = $"{store.Id},{store.Name},{store.Address}";
            await File.AppendAllLinesAsync(_filePath, [newLine]);
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            var lines = await File.ReadAllLinesAsync(_filePath);
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
            var lines = await File.ReadAllLinesAsync(_filePath);
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
