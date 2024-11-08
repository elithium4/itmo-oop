using ConsoleTables;
using MusicCatalog.Entities;
using MusicCatalog.Repositories;

namespace MusicCatalog.Services
{
    internal class MusiciansService : ExtendedEntityService<Musician>
    {
        //private UnitOfWork _unitOfWork;

        public MusiciansService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override void GetAll()
        {
            var musicians = _unitOfWork.Musicians.GetAll();
            if (musicians.Count > 0)
            {
                PrintAll(musicians);
            }
            else
            {
                Console.WriteLine("Нет добавленных исполнителей");
            }
        }

        public override void AddOne()
        {
            Console.WriteLine("Введите имя исполнителя");
            string name = Console.ReadLine();
            if (name.Length > 0)
            {
                if (_unitOfWork.Musicians.GetAll().Find(m => m.Name == name) != null)
                {
                    Console.WriteLine("Исполнитель с таким именем уже создан. Все равно добавить? 1 - да, 0 - нет");
                    if (!getConfirmation())
                    {
                        Console.WriteLine("Создание отменено");
                        return;
                    }
                }
                _unitOfWork.Musicians.Add(new Musician { Name = name });
                _unitOfWork.Save();
            }
            else
            {
                Console.WriteLine("Создание отменено");
            }

        }

        public override void DeleteOne()
        {
            if (!CheckIfDataPresent()) return;
            Console.WriteLine("Введите ID исполнителя для удаления");
            int name;
            if (int.TryParse(Console.ReadLine(), out name))
            {
                var entity = _unitOfWork.Musicians.Get(name);
                try
                {
                    _unitOfWork.Musicians.Delete(entity);
                    _unitOfWork.Save();
                    Console.WriteLine("Успешно удалено");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Не удалось удалить исполнителя");
                }
            }
            else
            {
                Console.WriteLine("Удаление отменено");
            }
        }

        public override void GetOne()
        {
            if (!CheckIfDataPresent()) return;
            Console.WriteLine("Введите ID исполнителя, либо -1 чтобы выйти");
            int id;
            Musician musician;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    if (id == -1)
                    {
                        return;
                    } else
                    {
                        musician = _unitOfWork.Musicians.Get(id);
                        if (musician == null)
                        {
                            Console.WriteLine("Нет исполнителя с таким ID");
                        } else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
            Console.WriteLine($"ID: {musician.Id}");
            Console.WriteLine($"Имя: {musician.Name}");
            Console.WriteLine($"Альбомов всего: {musician.Albums.Count}");
            Console.WriteLine($"Треков всего: {musician.Tracks.Count}");
            musician.Albums.ForEach(a =>
            {
                Console.WriteLine($"------{a.Name}------");
                int count = 1;
                a.Tracks.ForEach(t =>
                {
                    Console.WriteLine($"{count}. {t.Name}");
                    count++;
                });
            });
            var singles = musician.Tracks.Where(t => t.Album == null).ToList();
            if (singles.Count > 0)
            {
                Console.WriteLine("------Синглы------");
                singles.ForEach(t => Console.WriteLine($"{t.Name}"));
            }
        }

        public override bool CheckIfDataPresent() {
            var musicians = _unitOfWork.Musicians.GetAll();
            if (musicians.Count == 0)
            {
                Console.WriteLine("Нет добавленных исполнителей");
                return false;
            }
            return true;
        }

        public void Search()
        {
            if (!CheckIfDataPresent()) return;
            var musicians = _unitOfWork.Musicians.GetAll();
            Console.Write("Введите запрос или -1 для выхода: ");
            string query = Console.ReadLine();
            if (query == "-1")
            {
                return;
            }
            var results = musicians.Where(m => m.Name.ToLower().Contains(query.ToLower())).ToList();
            if (results.Count == 0)
            {
                Console.WriteLine("Нет результатов");
            }
            else
            {
                PrintAll(results);
            }
        }

        public override void PrintAll(List<Musician> entities)
        {
            var table = new ConsoleTable("ID", "Имя").Configure(o => o.EnableCount = false);

            entities.ForEach(e => table.AddRow(e.Id, e.Name));
            table.Write();
            Console.WriteLine();
        }

    }
}
