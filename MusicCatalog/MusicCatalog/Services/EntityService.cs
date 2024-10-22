using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Services
{
    internal abstract class EntityService
    {
        public abstract void GetAll();
        public abstract void DeleteOne();
        public abstract void AddOne();

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
