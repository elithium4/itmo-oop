
namespace MusicCatalog.Entities
{
    internal class Track : IHasName
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public List<Musician> Musicians { get; set; } = [];

        public Album? Album { get; set; }
        public List<TracksCollection> TrackCollections { get; set; } = [];

    }
}
