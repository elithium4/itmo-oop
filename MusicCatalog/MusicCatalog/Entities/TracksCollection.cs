
namespace MusicCatalog.Entities
{
    internal class TracksCollection : IHasName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Track> Tracks { get; set; } = [];
    }
}
