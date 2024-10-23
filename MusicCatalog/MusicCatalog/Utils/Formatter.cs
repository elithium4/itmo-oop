using MusicCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Utils
{
    internal static class Formatter
    {
       public static string FormatDuration(int pureSeconds)
        {
            int minutes = pureSeconds / 60;
            int seconds = pureSeconds % 60;
            return $"{minutes}:{seconds}";
        }

        public static string FormatArtists(List<Musician> musicians)
        {
            return string.Join(", ", musicians.Select(m => m.Name).ToArray());
        }
    }
}
