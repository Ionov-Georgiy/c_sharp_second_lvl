using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    static class StaticRandom
    {
        private static Random Rnd = new Random();
        public static int GetRandom(int max)
        {
            return Rnd.Next(max);
        }

        public static int GetRandom(int min, int max)
        {
            return Rnd.Next(min, max);
        }
    }
}
