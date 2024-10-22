using MusicCatalog;
using MusicCatalog.Entities;
using MusicCatalog.Repositories;
using MusicCatalog.Services;

using (MusicCatalogContext db = new MusicCatalogContext())
{
    var unitOfWork = new UnitOfWork(db);
    var interactionService = new InteractionService(unitOfWork);
    interactionService.Greet();
    interactionService.DisplayActionList();
    //var genres = db.Genres.ToList();
    //Console.WriteLine(genres.Count);
    //foreach (var item in genres)
    //{
    //    Console.WriteLine(item.Name);
    //}
    //var musician = new Musician { Id = 1, Name = "Waterparks" };
    //db.Musicians.Add(musician);
    //db.SaveChanges();
    //var all = db.Musicians.ToList();
    //Console.WriteLine(all.Count);
    //foreach (var item in all)
    //{
    //    Console.WriteLine(item.Name);
    //}
    // создаем два объекта User
    //User tom = new User { Name = "Tom", Age = 33 };
    //User alice = new User { Name = "Alice", Age = 26 };

    //// добавляем их в бд
    //db.Users.Add(tom);
    //db.Users.Add(alice);
    //db.SaveChanges();
    //Console.WriteLine("Объекты успешно сохранены");

    //// получаем объекты из бд и выводим на консоль
    //var users = db.Users.ToList();
    //Console.WriteLine("Список объектов:");
    //foreach (User u in users)
    //{
    //    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
    //}
}