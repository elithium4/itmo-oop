using ConsoleTables;
using MusicCatalog.Entities;
using MusicCatalog.Repositories;
using MusicCatalog.Utils;

namespace MusicCatalog.Services
{
    internal class AlbumService : ExtendedEntityService<Album>
    {
        //private UnitOfWork _unitOfWork;

        public AlbumService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override void GetAll()
        {
            var entities = _unitOfWork.Albums.GetAll();
            if (entities.Count > 0)
            {

                PrintAll(entities);
            }
            else
            {
                Console.WriteLine("Нет добавленных альбомов");
            }
        }

        public override void GetOne()
        {
            if (!CheckIfDataPresent()) return;
            Console.WriteLine("Введите ID альбома для получения информации");
            int id;
            Album album;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    if (id == -1)
                    {
                        return;
                    }
                    else
                    {
                        album = _unitOfWork.Albums.Get(id);
                        if (album == null)
                        {
                            Console.WriteLine("Нет альбома с таким ID");
                        }
                        else
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
            Console.WriteLine($"ID: {album.Id}");
            Console.WriteLine($"Имя: {album.Name}");
            Console.WriteLine($"Исполнители: {Formatter.FormatArtists(album.Musicians)}");
            Console.WriteLine($"Треков всего: {album.Tracks.Count}");
            int count = 1;
            album.Tracks.ForEach(t =>
            {
                Console.WriteLine($"{count}. {t.Name}");
                count++;
            });

        }

        public override void AddOne()
        {
            if (_unitOfWork.Musicians.GetAll().Count == 0)
            {
                Console.WriteLine("Необходимо добавить хотя бы одного исполнителя");
                return;
            }

            // Запрос названия
            Console.WriteLine("Введите название альбома");
            string name = Console.ReadLine();
            if (name.Length == 0)
            {
                Console.WriteLine("Создание отменено");
                return;
            }

            // Запрос авторов альбома (может быть несколько, должны существовать)
            List<Musician> musicians = new List<Musician>();
            bool hasAddedMusicians = false;
            List<Musician> allMusicians = _unitOfWork.Musicians.GetAll();

            while (!hasAddedMusicians && allMusicians.Count > musicians.Count)
            {
                Console.WriteLine(musicians.Count == 0 ? "Введите ID исполнителя" : "Введите ID исполнителя, либо -1 чтобы закончить выбор исполнителей");
                int musicianId;
                while (true)
                {
                    if (!int.TryParse(Console.ReadLine(), out musicianId) || (allMusicians.Find(m => m.Id == musicianId) == null && musicianId != -1))
                    {
                        Console.WriteLine("Попробуйте еще раз");
                    }
                    else
                    {
                        if (musicianId == -1)
                        {
                            if (musicians.Count > 0)
                            {
                                hasAddedMusicians = true;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Необходимо укзать хотя бы одного исполнителя.");
                            }
                        }
                        else
                        {
                            var musician = allMusicians.Find(m => m.Id == musicianId);
                            musicians.Add(musician);
                            Console.WriteLine($"Добавлен исполнитель: {musician.Name}");
                            break;

                        }
                    }
                }
            }

            // Запрос на добавление треков в альбом
            Console.WriteLine("Добавить треки сейчас? 1 - да, 0 - нет");
            List<Track> tracks = new List<Track>();
            if (getConfirmation())
            {
                tracks = GetSuitableTracks(musicians);
            }
            _unitOfWork.Albums.Add(new Album { Name = name, Musicians = musicians, Tracks = tracks });
            _unitOfWork.Save();
        }

        public override void DeleteOne()
        {
            if (!CheckIfDataPresent())
            {
                return;
            }
            Console.WriteLine("Введите ID альбома для удаления");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                var entity = _unitOfWork.Albums.Get(id);
                try
                {
                    _unitOfWork.Albums.Delete(entity);
                    _unitOfWork.Save();
                    Console.WriteLine("Успешно удалено");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Не удалось удалить альбом");
                }
            }
            else
            {
                Console.WriteLine("Удаление отменено");
            }
        }

        public void AddTracksToExisting()
        {
            if (!CheckIfDataPresent()) {
                return;
            }
            Console.WriteLine("Введите ID альбома, в который нужно добавить треки");
            int id;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out id) && _unitOfWork.Albums.Get(id) != null)
                {
                    var album = _unitOfWork.Albums.Get(id);
                    List<Track> tracks = GetSuitableTracks(album.Musicians);
                    album.Tracks.AddRange(tracks);
                    _unitOfWork.Save();
                    return;
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз");
                }
            }

        }

        private List<Track> GetSuitableTracks(List<Musician> musicians)
        {
            List<Track> tracks = new List<Track>();
            var suitableTracks = _unitOfWork.Tracks.GetAll().Where(t => musicians.Any(m => t.Musicians.Contains(m)) && t.Album == null).ToList();
            if (suitableTracks.Count == 0)
            {
                Console.WriteLine("Нет подходящих треков");
            }
            else
            {
                Console.WriteLine("Подходящие треки:");
                suitableTracks.ForEach(t =>
                {
                    Console.WriteLine($"{t.Id}      {t.Name}    {Formatter.FormatArtists(t.Musicians)}");
                });
                while (tracks.Count < suitableTracks.Count)
                {
                    Console.WriteLine("Введите ID трека для добавления, либо -1, чтобы закончить");
                    int trackId;
                    if (!int.TryParse(Console.ReadLine(), out trackId) || (trackId != -1 && suitableTracks.Find(t => t.Id == trackId) == null))
                    {
                        Console.WriteLine("Попробуйте еще раз");
                    }
                    else
                    {
                        if (trackId == -1)
                        {
                            break;
                        }
                        else
                        {
                            var track = suitableTracks.Find(t => t.Id == trackId);
                            tracks.Add(track);
                            Console.WriteLine($"Успешно добавлено: {track.Name}");
                        }
                    }
                }

            }
            return tracks;
        }

        public override bool CheckIfDataPresent() {
            var albums = _unitOfWork.Albums.GetAll();
            if (albums.Count == 0)
            {
                Console.WriteLine("Нет добавленных альбомов");
                return false;
            }
            return true;
        }

        public void Search()
        {
            var albums = _unitOfWork.Albums.GetAll();
            if (!CheckIfDataPresent())
            {
                return;
            }
            Console.Write("Введите запрос или -1 для выхода: ");
            string query = Console.ReadLine();
            if (query == "-1")
            {
                return;
            }
            var results = albums.Where(m => m.Name.ToLower().Contains(query.ToLower())).ToList();
            if (results.Count == 0)
            {
                Console.WriteLine("Нет результатов");
            }
            else
            {
                PrintAll(results);
            }
        }

        public override void PrintAll(List<Album> entities)
        {
            var table = new ConsoleTable("ID", "Название", "Исполнитель", "Треков всего").Configure(o => o.EnableCount = false);

            entities.ForEach(e => table.AddRow(e.Id, e.Name, Formatter.FormatArtists(e.Musicians), e.Tracks.Count));
            table.Write();
            Console.WriteLine();
        }
    }
}
