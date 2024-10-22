using MusicCatalog.Repositories;

namespace MusicCatalog.Services
{
    internal class InteractionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly GenresService _genresService;
        private readonly MusiciansService _musiciansService;
        private readonly TracksService _tracksService;
        private readonly AlbumService _albumService;
        private readonly TracksCollectionService _tracksCollectionService;
        public InteractionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _genresService = new GenresService(unitOfWork);
            _musiciansService = new MusiciansService(unitOfWork);
            _tracksService = new TracksService(unitOfWork);
            _albumService = new AlbumService(unitOfWork);
            _tracksCollectionService = new TracksCollectionService(unitOfWork);

        }
        public void Greet()
        {
            Console.WriteLine("Добро пожаловать в музыкальный каталог!");
        }
        public void DisplayActionList()
        {
            while (true)
            {
                Console.WriteLine("Выберите раздел для взаимодействия:");
                Console.WriteLine("1 - Альбомы");
                Console.WriteLine("2 - Треки");
                Console.WriteLine("3 - Исполнители");
                Console.WriteLine("4 - Сборники треков");
                Console.WriteLine("5 - Жанры");
                int action;
                if (int.TryParse(Console.ReadLine(), out action) && action >= 1 && action <= 5)
                {
                    switch (action)
                    {
                        case 1:
                            HandleAlbums();
                            break;
                        case 2:
                            HandleTracks();
                            break;
                        case 3:
                            HandleMusicians();
                            break;
                        case 4:
                            HandleCollections();
                            break;
                        case 5:
                            HandleGenres();
                            break;
                        default:
                            Console.WriteLine("Error");
                            break;
                    }
                } else
                {
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }

        public void HandleGenres()
        {
            var inMenu = true;
            while (inMenu)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Посмотреть список жанров");
                Console.WriteLine("2 - Добавить жанр");
                Console.WriteLine("3 - Удалить жанр");
                Console.WriteLine("4 - Вернуться в меню разделов");
                int action;
                if (int.TryParse(Console.ReadLine(), out action) && action >= 1 && action <= 4)
                {
                    switch (action)
                    {
                        case 1:
                            _genresService.GetAll();
                            break;
                        case 2:
                            _genresService.AddOne();
                            break;
                        case 3:
                            _genresService.DeleteOne();
                            break;
                        case 4:
                            inMenu = false;
                            break;
                        default:
                            Console.WriteLine("Попробуйте еще раз");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }

        public void HandleMusicians()
        {
            var inMenu = true;
            while (inMenu)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Посмотреть список исполнителей");
                Console.WriteLine("2 - Добавить исполнителя");
                Console.WriteLine("3 - Удалить исполнителя");
                Console.WriteLine("4 - Перейти к информации об исполнителе");
                Console.WriteLine("5 - Поиск по исполнителям");
                Console.WriteLine("6 - Вернуться в меню разделов");
                int action;
                if (int.TryParse(Console.ReadLine(), out action) && action >= 1 && action <= 6)
                {
                    switch (action)
                    {
                        case 1:
                            _musiciansService.GetAll();
                            break;
                        case 2:
                            _musiciansService.AddOne();
                            break;
                        case 3:
                            _musiciansService.DeleteOne();
                            break;
                        case 4:
                            _musiciansService.GetOne();
                            break;
                        case 5:
                            _musiciansService.Search();
                            break;
                        case 6:
                            inMenu = false;
                            break;
                        default:
                            Console.WriteLine("Попробуйте еще раз");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }

        public void HandleTracks()
        {
            var inMenu = true;
            while (inMenu)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Посмотреть список треков");
                Console.WriteLine("2 - Добавить трек");
                Console.WriteLine("3 - Удалить трек");
                Console.WriteLine("4 - Поиск по трекам");
                Console.WriteLine("5 - Вернуться в меню разделов");
                int action;
                if (int.TryParse(Console.ReadLine(), out action) && action >= 1 && action <= 5)
                {
                    switch (action)
                    {
                        case 1:
                            _tracksService.GetAll();
                            break;
                        case 2:
                            _tracksService.AddOne();
                            break;
                        case 3:
                            _tracksService.DeleteOne();
                            break;
                        case 4:
                            _tracksService.Search();
                            break;
                        case 5:
                            inMenu = false;
                            break;
                        default:
                            Console.WriteLine("Попробуйте еще раз");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }

        public void HandleAlbums()
        {
            var inMenu = true;
            while (inMenu)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Посмотреть список альбомов");
                Console.WriteLine("2 - Добавить альбом");
                Console.WriteLine("3 - Удалить альбом");
                Console.WriteLine("4 - Перейти к информации об альбоме");
                Console.WriteLine("5 - Добавить треки в существующий альбом");
                Console.WriteLine("6 - Поиск по альбомам");
                Console.WriteLine("7 - Вернуться в меню разделов");
                int action;
                if (int.TryParse(Console.ReadLine(), out action) && action >= 1 && action <= 7)
                {
                    switch (action)
                    {
                        case 1:
                            _albumService.GetAll();
                            break;
                        case 2:
                            _albumService.AddOne();
                            break;
                        case 3:
                            _albumService.DeleteOne();
                            break;
                        case 4:
                            _albumService.GetOne();
                            break;
                        case 5:
                            _albumService.AddTracksToExisting();
                            break;
                        case 6:
                            _albumService.Search();
                            break;
                        case 7:
                            inMenu = false;
                            break;
                        default:
                            Console.WriteLine("Попробуйте еще раз");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }

        public void HandleCollections()
        {
            var inMenu = true;
            while (inMenu)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Посмотреть список сборников");
                Console.WriteLine("2 - Добавить сборник");
                Console.WriteLine("3 - Удалить сборник");
                Console.WriteLine("4 - Перейти к информации о сборнике");
                Console.WriteLine("5 - Добавить треки в существующий сборник");
                Console.WriteLine("6 - Поиск по сборнику");
                Console.WriteLine("7 - Вернуться в меню разделов");
                int action;
                if (int.TryParse(Console.ReadLine(), out action) && action >= 1 && action <= 7)
                {
                    switch (action)
                    {
                        case 1:
                            _tracksCollectionService.GetAll();
                            break;
                        case 2:
                            _tracksCollectionService.AddOne();
                            break;
                        case 3:
                            _tracksCollectionService.DeleteOne();
                            break;
                        case 4:
                            _tracksCollectionService.GetOne();
                            break;
                        case 5:
                            _tracksCollectionService.AddMoreTracks();
                            break;
                        case 6:
                            _tracksCollectionService.Search();
                            break;
                        case 7:
                            inMenu = false;
                            break;
                        default:
                            Console.WriteLine("Попробуйте еще раз");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }
    }
}
