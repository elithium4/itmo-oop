﻿using MusicCatalog.Entities;
using MusicCatalog.Repositories;
using MusicCatalog.Utils;

namespace MusicCatalog.Services
{
    internal class TracksCollectionService: ExtendedEntityService
    {
        public TracksCollectionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override void GetAll()
        {
            Console.WriteLine("ID       Название        Треков всего");
            _unitOfWork.TracksCollections.GetAll().ForEach(c =>
            {
                Console.WriteLine($"{c.Id}      {c.Name}        {c.Tracks.Count}");
            });
        }

        public override void AddOne()
        {
            if (_unitOfWork.Tracks.GetAll().Count == 0)
            {
                Console.WriteLine("Необходимо добавить хотя бы один трек прежде чем создавать сборник");
                return;
            }

            // Запрос названия
            Console.WriteLine("Введите название сборника");
            string name = Console.ReadLine();
            if (name.Length == 0)
            {
                Console.WriteLine("Создание отменено");
                return;
            }
            Console.WriteLine("Добавить треки в сборник? 1 - да, 0 - нет");
            List<Track> tracks = [];
            if (getConfirmation())
            {
                GetSuitableTracks(tracks);
            }
            _unitOfWork.TracksCollections.Add(new TracksCollection { Name = name, Tracks = tracks });
            _unitOfWork.Save();
        }


        public void AddMoreTracks()
        {
            Console.WriteLine("Введите ID сборника для получения информации или -1 для выхода");
            int id;
            TracksCollection collection;
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
                        collection = _unitOfWork.TracksCollections.Get(id);
                        if (collection == null)
                        {
                            Console.WriteLine("Нет сборника с таким ID");
                        }
                        else
                        {
                            GetSuitableTracks(collection.Tracks);
                            _unitOfWork.Save();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }
        private void GetSuitableTracks(List<Track> currentTracks)
        {
            var allTracks = _unitOfWork.Tracks.GetAll().Where(t => currentTracks.Find(ct => ct.Id == t.Id) == null).ToList();
            if (allTracks.Count == 0)
            {
                Console.WriteLine("Нет подходящих треков");
            } else
            {
                bool isAddingTracks = true;
                while (isAddingTracks && allTracks.Count > 0)
                {
                    Console.WriteLine("Введите ID трека для добавления, -1 чтобы закончить, -2 чтобы посмотреть список доступных треков");
                    int id;
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        switch (id)
                        {
                            case -1:
                                isAddingTracks = false;
                                break;
                            case -2:
                                allTracks.ForEach(t => Console.WriteLine($"{t.Id}   {t.Name}    {Formatter.FormatArtists(t.Musicians)}"));
                                break;
                            default:
                                var track = allTracks.Find(t => t.Id == id);
                                if (track != null)
                                {
                                    allTracks.Remove(track);
                                    currentTracks.Add(track);
                                    Console.WriteLine($"Добавлено: {track.Name}");
                                }
                                else
                                {
                                    Console.WriteLine("Трек с указанным ID недоступен");
                                }
                                break;
                        }
                    } else
                    {
                        Console.WriteLine("Попробуйте еще раз");
                    }
                }

            }
        }

        public override void DeleteOne()
        {
            if (_unitOfWork.TracksCollections.GetAll().Count == 0)
            {
                Console.WriteLine("Нет сборников, чтобы что-то удалять");
                return;
            }
            Console.WriteLine("Введите ID сборника для удаления");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                var entity = _unitOfWork.TracksCollections.Get(id);
                try
                {
                    _unitOfWork.TracksCollections.Delete(entity);
                    _unitOfWork.Save();
                    Console.WriteLine("Успешно удалено");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Не удалось удалить сборник");
                }
            }
            else
            {
                Console.WriteLine("Удаление отменено");
            }
        }

        public override void GetOne()
        {
            Console.WriteLine("Введите ID сборника для получения информации или -1 для выхода");
            int id;
            TracksCollection collection;
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
                        collection = _unitOfWork.TracksCollections.Get(id);
                        if (collection == null)
                        {
                            Console.WriteLine("Нет сборника с таким ID");
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
            Console.WriteLine($"ID: {collection.Id}");
            Console.WriteLine($"Имя: {collection.Name}");
            Console.WriteLine($"Треков всего: {collection.Tracks.Count}");
            int count = 1;
            collection.Tracks.ForEach(t =>
            {
                Console.WriteLine($"{count}. {t.Name}");
                count++;
            });
        }

        public void Search()
        {
            var collections = _unitOfWork.TracksCollections.GetAll();
            if (collections.Count == 0)
            {
                Console.WriteLine("Нет сборников для поиска");
                return;
            }
            Console.Write("Введите запрос или -1 для выхода: ");
            string query = Console.ReadLine();
            if (query == "-1")
            {
                return;
            }
            var results = collections.Where(m => m.Name.ToLower().Contains(query.ToLower())).ToList();
            if (results.Count == 0)
            {
                Console.WriteLine("Нет результатов");
            }
            else
            {
                results.ForEach(m => Console.WriteLine($"{m.Id} {m.Name}"));
            }

        }
    }
}