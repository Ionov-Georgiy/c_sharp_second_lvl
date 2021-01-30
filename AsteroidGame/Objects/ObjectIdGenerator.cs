using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    internal static class ObjectIdGenerator
    {
        private static int Id = 0;
        public static int GenerateId()
        {
            return ++Id;
        }
    }
}
