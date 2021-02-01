using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame
{
    abstract class ObjectInSpace 
    {

        protected Point Position;
        protected Point Direction;
        protected Size ObjectSize;

        public ObjectInSpace(Point Position, Point Direction, Size ObjectSize)
        {
            this.Position = Position;
            this.Direction = Direction;
            this.ObjectSize = ObjectSize;
        }

        public virtual void Draw(Graphics g)
        {
            g.DrawEllipse(
                Pens.White,
                Position.X, Position.Y,
                ObjectSize.Width, ObjectSize.Height);
        }

        public virtual void Move(Size SpaceSize)
        {
            Position.X += Direction.X;
            Position.Y += Direction.Y;

            if (Position.X < 0)
                Direction.X *= -1;
            if (Position.Y < 0)
                Direction.Y *= -1;

            if (Position.X > SpaceSize.Width - ObjectSize.Width)
                Direction.X *= -1;
            if (Position.Y > SpaceSize.Height - ObjectSize.Height)
                Direction.Y *= -1;
        }

    }
}
