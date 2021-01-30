using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Interfaces
{
    interface IUIElement
    {
        Size ElementSize { get; }
        void Draw(Graphics g, Point ElementLocation);

    }
}
