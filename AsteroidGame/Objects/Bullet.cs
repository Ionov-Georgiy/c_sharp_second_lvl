﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    class Bullet : ObjectInSpace, ICollisionable
    {

        private const int Bullet_Size_X = 100;
        private const int Bullet_Size_Y = 5;
        private const int Bullet_Speed = 3;

        override public void DoCollisionConsequences(ICollisionable CollisionObject)
        {
            //Nothing to do here
        }

        public Bullet(int Position, Space ObjectSpace)
            : base(new Point(0, Position), Point.Empty, new Size(Bullet_Size_X, Bullet_Size_Y), ObjectSpace)
        {

        }

        override public void MoveInSpace()
        {
            Position.X += Bullet_Speed;
            if (Position.X > ObjectSpace.SpaceSize.Width)
                Position.X = 0;
        }

        override public void Draw(Graphics g)
        {
            var rect = Rect;
            g.FillEllipse(Brushes.Red, rect);
            g.DrawEllipse(Pens.White, rect);
        }
    }
}