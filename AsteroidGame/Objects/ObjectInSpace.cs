using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    class ObjectInSpace : IMovableInSpace, ICollisionable
    {
        public Space ObjectSpace { get { return objectSpace; } }
        protected Space objectSpace;
        protected Point Position;
        public Point Direction { get { return direction; } }
        protected Point direction;
        protected Size ObjectSize;
        public Rectangle Rect { get { return new Rectangle(Position, ObjectSize); } }

        public ObjectInSpace(Point Position, Point Direction, Size ObjectSize, Space ObjectSpace)
        {
            this.objectSpace = ObjectSpace;
            this.Position = Position;
            this.direction = Direction;
            this.ObjectSize = ObjectSize;
            CheckParams();
        }

        private void CheckParams()
        {
            Rectangle SpaceRect = new Rectangle(new Point(0, 0), new Size(ObjectSpace.SpaceSize.Width + ObjectSize.Width, ObjectSpace.SpaceSize.Height + ObjectSize.Height));
            //if (!SpaceRect.IntersectsWith(Rect))
            //{
            //    throw new GameObjectException("Position", "Позиция объекта находиться вне пространства");
            //}
            if(Math.Abs(direction.X) > ObjectSize.Width*2 || Math.Abs(direction.Y) > ObjectSize.Height*2)
            {
                throw new GameObjectException("Direction", "Значение модификатора направления Y или X превышает размер объекта (ObjectSize)");
            }
            if(ObjectSpace.SpaceSize.Width < 0 ||
               ObjectSpace.SpaceSize.Height < 0 ||
               ObjectSpace.SpaceSize.Width / 4 < ObjectSize.Width || 
               ObjectSpace.SpaceSize.Height / 4 < ObjectSize.Height)
            {
                throw new GameObjectException("ObjectSize", "Размер объекта превышает или неверно указан");
            }
        }

        public virtual void Draw(Graphics g)
        {
            g.DrawEllipse(
                Pens.White,
                Position.X, Position.Y,
                ObjectSize.Width, ObjectSize.Height);
        }

        public virtual void MoveInSpace()
        {
            Position.X += Direction.X;
            Position.Y += Direction.Y;

            if (Position.X < 0)
                direction.X *= -1;
            if (Position.Y < 0)
                direction.Y *= -1;

            if (Position.X > ObjectSpace.SpaceSize.Width - ObjectSize.Width)
                direction.X *= -1;
            if (Position.Y > ObjectSpace.SpaceSize.Height - ObjectSize.Height)
                direction.Y *= -1;
        }

        public bool CheckCollision(ICollisionable obj)
        {
            return Rect.IntersectsWith(obj.Rect);
        }

        virtual public void DoCollisionConsequences(ICollisionable obj)
        {
            Point CollisionObjectDirection = obj.Direction;
            if (direction.X > 0 || CollisionObjectDirection.X < 0)
            {
                direction.X = CollisionObjectDirection.X * -1;
            }
            else
            {
                direction.X = CollisionObjectDirection.X;
            }

            if (direction.Y > 0 || CollisionObjectDirection.Y < 0)
            {
                direction.Y = CollisionObjectDirection.Y * -1;
            }
            else
            {
                direction.Y = CollisionObjectDirection.Y;
            }
        }

    }
}
