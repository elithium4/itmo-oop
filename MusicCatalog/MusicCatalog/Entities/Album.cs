
namespace MusicCatalog.Entities
{
    internal class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Track> Tracks { get; } = [];

        public List<Musician> Musicians { get; set; } = [];
    }
}
