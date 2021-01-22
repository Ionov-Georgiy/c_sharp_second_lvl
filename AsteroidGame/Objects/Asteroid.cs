using AsteroidGame.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    class Asteroid : ObjectInSpace, ICollisionable
    {

        private Image astrImg;

        override public void DoCollisionConsequences(ICollisionable obj)
        {
            if(obj is Asteroid)
            {
                base.DoCollisionConsequences(obj);
                astrImg.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            else if(obj is Bullet)
            {
                direction = new Point(-StaticRandom.GetRandom(1, 20), StaticRandom.GetRandom(20));
                Position = new Point(800 - ObjectSize.Width, StaticRandom.GetRandom(1, 600));
            }
        }

        public Asteroid(Point Position, Point Direction, int Size, Space ObjectSpace)
            : base(Position, Direction, new Size(Size, Size), ObjectSpace)
        {
            astrImg = Image.FromFile("Asteroid.png");
        }

        override public void MoveInSpace()
        {
            Position.X += direction.X;
            Position.Y += direction.Y;

            if (Position.X < 0)
            {
                direction.X *= -1;
                astrImg.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            if (Position.Y < 0 + ObjectSize.Height)
            {
                direction.Y *= -1;
                astrImg.RotateFlip(RotateFlipType.Rotate180FlipY);
            }

            if (Position.X + ObjectSize.Width > ObjectSpace.SpaceSize.Width)
            {
                direction.X *= -1;
                astrImg.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            if (Position.Y + ObjectSize.Height > ObjectSpace.SpaceSize.Height)
            {
                if(direction.Y > 0)
                    direction.Y *= -1;
                astrImg.RotateFlip(RotateFlipType.Rotate180FlipY);
            }
        }

        override public void Draw(Graphics g)
        {
            g.DrawImage(astrImg, new Point(Position.X, Position.Y));
        }

        private void DoBulletCollisionConsequences(ICollisionable obj)
        {

        }

    }
}
