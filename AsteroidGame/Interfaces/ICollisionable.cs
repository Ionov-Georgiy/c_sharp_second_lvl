using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame.Interfaces
{
    interface ICollisionable
    {
        int Id { get; }

        Rectangle Rect { get; }

        Point Direction { get; }

        bool CheckCollision(ICollisionable obj);

        void DoCollisionConsequences(ICollisionable obj);

    }
}
