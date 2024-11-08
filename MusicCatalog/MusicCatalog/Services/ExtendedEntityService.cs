
namespace MusicCatalog.Services
{
    internal abstract class ExtendedEntityService<T>: EntityService<T> where T : class
    {
        public abstract void GetOne();
    }
}
