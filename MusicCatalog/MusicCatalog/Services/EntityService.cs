using MusicCatalog.Repositories;

namespace MusicCatalog.Services
{
    internal abstract class EntityService
    {
        protected UnitOfWork _unitOfWork;
        public abstract void GetAll();
        public abstract void DeleteOne();
        public abstract void AddOne();

        public abstract bool CheckIfDataPresent();

        public bool getConfirmation()
        {
            while (true)
            {
                int action;
                if (int.TryParse(Console.ReadLine(), out action) && (action == 1 || action == 0))
                {
                    return action == 1;
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }
    }
}
