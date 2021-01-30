using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidGame.GameLogic;
using AsteroidGame.Interfaces;

namespace AsteroidGame.Objects
{
    class EnergyBar : IUIElement
    {
        private Size barSize = new Size(100, 10);
        private string barName = "Energy:";

        public Size ElementSize => elementSize;
        private Size elementSize = new Size(100, 10);
        public int CurrentEnergy;
        public int MaxEnergy;
        public event EventHandler StartDrawing;

        public EnergyBar(int CurrentEnergy, int MaxEnergy)
        {
            this.CurrentEnergy = CurrentEnergy;
            this.MaxEnergy = MaxEnergy;
        }

        public void Draw(Graphics g, Point ElementLocation)
        {
            StartDrawing.Invoke(this, EventArgs.Empty);
            Font strFont = new Font(FontFamily.GenericMonospace, 14);
            g.DrawString(barName, strFont, Brushes.Green, ElementLocation.X, ElementLocation.Y);
            g.DrawRectangle(Pens.Red, strFont.Size*barName.Length + 10 + ElementLocation.X, ElementLocation.Y + (strFont.Height - barSize.Height)/3, ElementSize.Width, ElementSize.Height);
            g.FillRectangle(Brushes.Red, strFont.Size * barName.Length + 10 + ElementLocation.X, ElementLocation.Y + (strFont.Height - barSize.Height) / 3, CurrentEnergy*barSize.Width/MaxEnergy, barSize.Height);
        }

    }
}
