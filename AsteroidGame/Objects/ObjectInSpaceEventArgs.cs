using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    class ObjectInSpaceEventArgs : EventArgs
    {
        public object CollisionObject;

        public ObjectInSpaceEventArgs(object CollisionObject)
        {
            this.CollisionObject = CollisionObject;
        }

    }
}
