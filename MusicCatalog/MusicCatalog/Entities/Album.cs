
namespace MusicCatalog.Entities
{
    internal class Album: IHasName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Track> Tracks { get; set; } = [];

        public List<Musician> Musicians { get; set; } = [];
    }
}
