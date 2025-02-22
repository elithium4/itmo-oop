﻿using ConsoleTables;
using MusicCatalog.Entities;
using MusicCatalog.Repositories;
using MusicCatalog.Utils;

namespace MusicCatalog.Services
{
    internal class TracksService: EntityService<Track>
    {

        public TracksService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Добавление трека
        public override void AddOne()
        {
            if (_unitOfWork.Musicians.GetAll().Count == 0 || _unitOfWork.Genres.GetAll().Count == 0)
            {
                Console.WriteLine("Перед добавлением трека необходимо добавить хотя бы одного исполнителя и хотя бы один жанр");
                return;
            }

            // Запрос названия
            Console.WriteLine("Введите название трека");
            string name = Console.ReadLine();
            if (name.Length == 0)
            {
                Console.WriteLine("Создание отменено");
                return;
            }

            Console.WriteLine("Введите длительность трека (в секундах)");
            int duration;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out duration) && duration > 0)
                {
                    break;
                } else
                {
                    Console.WriteLine("Попробуйте снова");
                }
            }
            if (name.Length == 0)
            {
                Console.WriteLine("Создание отменено");
                return;
            }

            // Запрос жанра (должен существовать)
            Console.WriteLine("Введите ID жанра трека");
            int genreId;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out genreId) || _unitOfWork.Genres.Get(genreId) == null)
                {
                    Console.WriteLine("Попробуйте еще раз");
                } else
                {
                    break;
                }
            }
            var genre = _unitOfWork.Genres.Get(genreId);

            // Запрос авторов трека (может быть несколько, должны существовать)
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
                            } else
                            {
                                Console.WriteLine("Необходимо укзать хотя бы одного исполнителя.");
                            }
                        } else
                        {
                            musicians.Add(allMusicians.Find(m => m.Id == musicianId));
                            break;

                        }
                    }
                }
            }

            // Запрос альбома для трека (должен существовать, может быть только один)
            Console.WriteLine("Добавить трек в альбом? 1 - да, 0 - нет");
            Album album = null;
            if (getConfirmation())
            {
                var suitableAlbums = _unitOfWork.Albums.GetAll().Where(a => musicians.All(m => a.Musicians.Contains(m))).ToList();
                if (suitableAlbums.Count > 0)
                {
                    Console.WriteLine("Подходящие альбомы:");
                    Console.WriteLine("ID           Name");
                    suitableAlbums.ForEach(a => Console.WriteLine($"{a.Id}      {a.Name}"));
                    Console.WriteLine("Введите ID альбома, либо -1, чтобы отменить добавление");
                    int albumId;
                    while (true)
                    {
                        if (
                            !int.TryParse(Console.ReadLine(), out albumId)
                            || suitableAlbums.Find(a => a.Id == albumId) == null && albumId != -1)
                        {
                            Console.WriteLine("Попробуйте еще раз");
                        }
                        else
                        {
                            if (albumId == -1) break;
                            album = suitableAlbums.Find(a => a.Id == albumId);
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Не найдено подходящих альбомов");
                }
            }

             _unitOfWork.Tracks.Add(new Track { Name = name, Genre = genre, Duration = duration, Album = album, Musicians = musicians });
            _unitOfWork.Save();
        }

        public override void GetAll()
        {
            var entities = _unitOfWork.Tracks.GetAll();
            if (entities.Count > 0)
            {
                PrintAll(entities);
                
            }
            else
            {
                Console.WriteLine("Нет добавленных треков");
            }

        }
        public override void DeleteOne()
        {
            if (!CheckIfDataPresent()) return;
            Console.WriteLine("Введите ID трека для удаления");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                var entity = _unitOfWork.Tracks.Get(id);
                try
                {
                    _unitOfWork.Tracks.Delete(entity);
                    _unitOfWork.Save();
                    Console.WriteLine("Успешно удалено");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Не удалось удалить жанр");
                }
            }
            else
            {
                Console.WriteLine("Удаление отменено");
            }
        }

        public override bool CheckIfDataPresent() {
            if (_unitOfWork.Tracks.GetAll().Count == 0)
            {
                Console.WriteLine("Нет добавленных треков");
                return false;
            }
            return true;
        }
        public void Search()
        {
            if (!CheckIfDataPresent()) return;
            Console.WriteLine("Выберите критерий поиска:");
            Console.WriteLine("1 - по названию");
            Console.WriteLine("2 - по исполнителю");
            Console.WriteLine("3 - по жанру");
            Console.WriteLine("Для выхода введите любое другое значение");
            int action;
            if (int.TryParse(Console.ReadLine(), out action) && action >= 1 && action <= 3)
            {
                switch (action)
                {
                    case 1:
                        SearchByName();
                        break;
                    case 2:
                        SearchByMusician();
                        break;
                    case 3:
                        SearchByGenre();
                        break;
                    default:
                        Console.WriteLine("Неизвестный критерий");
                        break;
                }
            }
        }

        private void SearchByName()
        {
            Console.Write("Введите запрос: ");
            string query = Console.ReadLine().ToLower();
            var tracks = _unitOfWork.Tracks.GetAll().Where(t => t.Name.ToLower().Contains(query)).ToList();
            if (tracks.Count == 0)
            {
                Console.WriteLine("Нет результатов");
            } else
            {
                PrintAll(tracks);
            }
        }

        private void SearchByMusician()
        {
            Console.Write("Введите запрос: ");
            string query = Console.ReadLine().ToLower();
            var tracks = _unitOfWork.Tracks.GetAll().Where(t => t.Musicians.Any(m => m.Name.ToLower().Contains(query))).ToList();
            if (tracks.Count == 0)
            {
                Console.WriteLine("Нет результатов");
            }
            else
            {
                PrintAll(tracks);
            }
        }

        private void SearchByGenre()
        {
            Console.Write("Введите название жанра");
            string genre = Console.ReadLine().ToLower();
            var tracks = _unitOfWork.Tracks.GetAll().Where(t => t.Genre.Name == genre).ToList();
            if (tracks.Count == 0)
            {
                Console.WriteLine("Нет результатов");
            }
            else
            {
                PrintAll(tracks);
            }
        }

        public override void PrintAll(List<Track> tracks)
        {
            var table = new ConsoleTable("ID", "Название", "Длительность", "Альбом", "Исполнители").Configure(o => o.EnableCount = false);

            tracks.ForEach(e => table.AddRow(e.Id, e.Name, Formatter.FormatDuration(e.Duration), e.Album != null ? e.Album.Name : "-", Formatter.FormatArtists(e.Musicians)));
            table.Write();
            Console.WriteLine();
        }

    }
}
