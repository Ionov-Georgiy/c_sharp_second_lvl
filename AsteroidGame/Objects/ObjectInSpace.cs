using AsteroidGame.Interfaces;
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
        public int Id => id;
        private int id;
        public bool Enabled;
        public Space ObjectSpace => objectSpace;
        protected Space objectSpace;
        public Point Position => position;
        protected Point position;
        public Point Direction => direction;
        protected Point direction;
        protected Size ObjectSize;
        public Rectangle Rect => new Rectangle(Position, ObjectSize);

        public event EventHandler<ObjectInSpaceEventArgs> Collisionated;

        public ObjectInSpace(Point Position, Point Direction, Size ObjectSize, Space ObjectSpace, int Id)
        {
            id = Id;
            this.objectSpace = ObjectSpace;
            this.position = Position;
            this.direction = Direction;
            this.ObjectSize = ObjectSize;
            this.Enabled = true;
            CheckParams();
        }

        private void CheckParams()
        {
            Rectangle SpaceRect = new Rectangle(new Point(0, 0), new Size(ObjectSpace.SpaceSize.Width + ObjectSize.Width, ObjectSpace.SpaceSize.Height + ObjectSize.Height));
            //if (!SpaceRect.IntersectsWith(Rect))
            //{
            //    throw new GameObjectException("Position", "Позиция объекта находиться вне пространства", this);
            //}
            if (Math.Abs(direction.X) > ObjectSize.Width*2 || Math.Abs(direction.Y) > ObjectSize.Height*2)
            {
                throw new GameObjectException("Direction", "Значение модификатора направления Y или X превышает размер объекта (ObjectSize)", this);
            }
            if(ObjectSpace.SpaceSize.Width < 0 ||
               ObjectSpace.SpaceSize.Height < 0 ||
               ObjectSpace.SpaceSize.Width / 4 < ObjectSize.Width || 
               ObjectSpace.SpaceSize.Height / 4 < ObjectSize.Height)
            {
                throw new GameObjectException("ObjectSize", "Размер объекта превышает или неверно указан", this);
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
            position.X += Direction.X;
            position.Y += Direction.Y;

            if (Position.X < 0)
                direction.X *= -1;
            if (Position.Y < 0)
                direction.Y *= -1;

            if (Position.X > ObjectSpace.SpaceSize.Width - ObjectSize.Width)
                direction.X *= -1;
            if (Position.Y > ObjectSpace.SpaceSize.Height - ObjectSize.Height)
                direction.Y *= -1;
        }

        public virtual void MoveInSpace(Point Position)
        {
            position.X = Position.X;
            position.Y = Position.Y;

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
            if (Rect.IntersectsWith(obj.Rect))
            {
                Collisionated?.Invoke(this, new ObjectInSpaceEventArgs(obj));
                return true;
            }
            return false;
        }

        public virtual void DoCollisionConsequences(ICollisionable obj)
        {
            Point CollisionObjectDirection = obj.Direction;
            if (direction.X > 0 && CollisionObjectDirection.X < 0 || direction.X < 0 && CollisionObjectDirection.X > 0)
            {
                direction.X = CollisionObjectDirection.X * -1;
            }
            else
            {
                direction.X = CollisionObjectDirection.X;
            }

            if (direction.Y > 0 && CollisionObjectDirection.Y < 0 || direction.Y < 0 && CollisionObjectDirection.Y > 0)
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
