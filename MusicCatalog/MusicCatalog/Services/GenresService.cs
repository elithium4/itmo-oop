﻿using MusicCatalog.Entities;
using MusicCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Services
{
    internal class GenresService : EntityService
    {
        private UnitOfWork _unitOfWork;

        public GenresService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override void AddOne()
        {
            Console.WriteLine("Введите название жанра");
            string name = Console.ReadLine();
            if (name.Length > 0)
            {

                if (_unitOfWork.Musicians.GetAll().Find(m => m.Name == name) != null)
                {
                    Console.WriteLine("Жанр с таким названием уже создан. Все равно добавить? 1 - да, 0 - нет");
                    if (!getConfirmation())
                    {
                        Console.WriteLine("Создание отменено");
                        return;
                    }
                }
                _unitOfWork.Genres.Add(new Genre { Name = name });
                _unitOfWork.Save();
            }
            else
            {
                Console.WriteLine("Создание отменено");
            }
        }

        public override void GetAll()
        {
            var genres = _unitOfWork.Genres.GetAll();
            if (genres.Count > 0)
            {
                Console.WriteLine("ID        Name");
                genres.ForEach(g => Console.WriteLine($"{g.Id}         {g.Name}"));
            } else
            {
                Console.WriteLine("Нет сохраненных жанров");
            }
        
        }
        public override void DeleteOne()
        {
            Console.WriteLine("Введите ID жанра для удаления");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                var genre = _unitOfWork.Genres.Get(id);
                try
                {
                    _unitOfWork.Genres.Delete(genre);
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
    }
}
