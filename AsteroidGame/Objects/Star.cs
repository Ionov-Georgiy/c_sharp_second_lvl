using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame.Objects
{
    class Star : ObjectInSpace
    {

        private StarType type;
        private int starBrightness;

        public Star(Point Position, Point Direction, int Size, StarType StarType, int StarBrightness, Space ObjectSpace)
            : base(Position, Direction, new Size(Size, Size), ObjectSpace)
        {
            type = StarType;
            starBrightness = StarBrightness;
        }

        public override void Draw(Graphics g)
        {
            Pen StarColor = Pens.WhiteSmoke;
            //if (starBrightness == 1)
            //{
            //    StarColor = Pens.Gray;
            //}
            //else if(starBrightness == 2)
            //{
            //    StarColor = Pens.Blue;
            //}
            //else if (starBrightness == 3)
            //{
            //    StarColor = Pens.White;
            //}
            //else if (starBrightness == 4)
            //{
            //    StarColor = Pens.Orange;
            //}
            //else if (starBrightness == 5)
            //{
            //    StarColor = Pens.Red;
            //}

            if (type == StarType.Normal || type == StarType.Shiny)
            {
                g.DrawLine(StarColor,
                Position.X, Position.Y,
                Position.X + ObjectSize.Width, Position.Y + ObjectSize.Width);

                g.DrawLine(StarColor,
                    Position.X + ObjectSize.Width, Position.Y,
                    Position.X, Position.Y + ObjectSize.Width);
            }

            if (type == StarType.Shiny || type == StarType.Faint || type == StarType.LittleOne)
            {
                int XWidth = (int)((double)ObjectSize.Width / 1.618d);
                int PosX = Position.X + (int)Math.Round(((double)ObjectSize.Width - (double)ObjectSize.Width / 1.618d)/2);
                int YHeight = (int)((double)ObjectSize.Height / 1.618d);
                int PosY = Position.Y + (int)Math.Round(((double)ObjectSize.Height - (double)ObjectSize.Height / 1.618d)/2);

                g.DrawLine(StarColor,
                PosX + XWidth / 2, PosY,
                PosX + XWidth / 2, PosY + XWidth);

                g.DrawLine(StarColor,
                PosX, PosY + YHeight / 2,
                PosX + XWidth, PosY + YHeight / 2);
            }

            if (type == StarType.Faint || type == StarType.ShinyLittleOne)
            {
                int XWidth = (int)((double)ObjectSize.Width / 1.618d);
                int PosX = Position.X + (int)Math.Round(((double)ObjectSize.Width - (double)ObjectSize.Width / 1.618d) / 2);
                int YHeight = (int)((double)ObjectSize.Height / 1.618d);
                int PosY = Position.Y + (int)Math.Round(((double)ObjectSize.Height - (double)ObjectSize.Height / 1.618d) / 2);

                g.DrawLine(StarColor,
                    PosX, PosY,
                    PosX + XWidth, PosY + XWidth);

                g.DrawLine(StarColor,
                    PosX + XWidth, PosY,
                    PosX, PosY + XWidth);
            }

        }

        public override void MoveInSpace()
        {
            position.X += Direction.X;
            if (Position.X < 0)
            {
                position.X = ObjectSpace.SpaceSize.Width + ObjectSize.Width;
                position.Y = StaticRandom.GetRandom(ObjectSize.Height/2, ObjectSpace.SpaceSize.Height - ObjectSize.Height/2);
            }
        }
    }
}
