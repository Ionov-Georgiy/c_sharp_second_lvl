using AsteroidGame.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    class EnergyKit : ObjectInSpace, ICollisionable
    {
        public int Power { get; set; } = 5;
        private static int size = 24;

        override public void DoCollisionConsequences(ICollisionable obj)
        {
            if(obj is SpaceShip)
            {
                position.X = ObjectSpace.SpaceSize.Width + ObjectSize.Width;
                position.Y = StaticRandom.GetRandom(ObjectSize.Height / 2, ObjectSpace.SpaceSize.Height - ObjectSize.Height / 2);
            }
        }

        public EnergyKit(Point Position, Point Direction, Space ObjectSpace)
            : base(Position, Direction, new Size(size, size), ObjectSpace)
        {
            
        }

        override public void MoveInSpace()
        {
            position.X += Direction.X;
            if (position.X < 0)
            {
                position.X = ObjectSpace.SpaceSize.Width + ObjectSize.Width;
                position.Y = StaticRandom.GetRandom(ObjectSize.Height / 2, ObjectSpace.SpaceSize.Height - ObjectSize.Height / 2);
            }
        }

        override public void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Red, new Rectangle(new Point(Position.X, Position.Y + ObjectSize.Height/3), new Size(ObjectSize.Width, ObjectSize.Height / 3)));
            g.DrawRectangle(Pens.Red, new Rectangle(new Point(Position.X + ObjectSize.Width / 3, Position.Y), new Size(ObjectSize.Width / 3, ObjectSize.Height)));
        }

    }
}
