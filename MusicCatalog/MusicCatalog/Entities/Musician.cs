
namespace MusicCatalog.Entities
{
    internal class Musician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Track> Tracks { get; set; } = [];

        public List<Album> Albums { get; set; } = [];
    }
}
