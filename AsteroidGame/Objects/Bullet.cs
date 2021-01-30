using AsteroidGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    class Bullet : ObjectInSpace, ICollisionable
    {

        private const int Bullet_Size_X = 20;
        private const int Bullet_Size_Y = 5;
        private const int Bullet_Speed = 10;

        override public void DoCollisionConsequences(ICollisionable CollisionObject)
        {
            if (CollisionObject is Asteroid)
                this.Enabled = false;
        }

        public Bullet(int Position, Space ObjectSpace, int Id)
            : base(new Point(0, Position), Point.Empty, new Size(Bullet_Size_X, Bullet_Size_Y), ObjectSpace, Id)
        {

        }

        override public void MoveInSpace()
        {
            position.X += Bullet_Speed;
            if (position.X > ObjectSpace.SpaceSize.Width)
                Enabled = false;
        }

        override public void Draw(Graphics g)
        {
            var rect = Rect;
            g.FillEllipse(Brushes.Red, rect);
            g.DrawEllipse(Pens.White, rect);
        }
    }
}
