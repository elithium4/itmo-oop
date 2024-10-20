
namespace MusicCatalog.Entities
{
    internal class Track
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public List<Musician> Musicians { get; } = [];

        public Album? Album { get; set; }

    }
}
