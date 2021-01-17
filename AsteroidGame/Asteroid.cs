using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame
{
    class Asteroid : ObjectInSpace
    {

        private Image astrImg;

        public Asteroid(Point Position, Point Direction, int Size)
            : base(Position, Direction, new Size(Size, Size))
        {
            astrImg = Image.FromFile("Asteroid.png");
        }

        override public void Move(Size SpaceSize)
        {
            Position.X += Direction.X;
            Position.Y += Direction.Y;

            if (Position.X < 0)
            {
                Direction.X *= -1;
                astrImg.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            if (Position.Y < 0 + ObjectSize.Height)
            {
                Direction.Y *= -1;
                astrImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            if (Position.X + ObjectSize.Width > SpaceSize.Width)
            {
                Direction.X *= -1;
                astrImg.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            if (Position.Y + ObjectSize.Height > SpaceSize.Height)
            {
                Direction.Y *= -1;
                astrImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
        }

        override public void Draw(Graphics g)
        {
            g.DrawImage(astrImg, new Point(Position.X, Position.Y));
        }
    }
}
