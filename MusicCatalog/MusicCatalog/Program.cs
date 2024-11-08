using MusicCatalog;
using MusicCatalog.Repositories;
using MusicCatalog.Services;

using (MusicCatalogContext db = new MusicCatalogContext())
{
    var unitOfWork = new UnitOfWork(db);
    var interactionService = new InteractionService(unitOfWork);
    interactionService.Greet();
    interactionService.DisplayActionList();
}