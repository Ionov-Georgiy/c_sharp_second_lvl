using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame
{
    class Space
    {
        private int starsAmount;
        private int asteroidsAmount;
        private Size size { get; set; }
        private List<ObjectInSpace> backgroundObjects;
        private List<ObjectInSpace> foregroundObjects;

        public Space(Size size, int starsAmount, int asteroidsAmount)
        {
            this.size = size;
            this.starsAmount = starsAmount;
            this.asteroidsAmount = asteroidsAmount;
            FillInSpace();
        }

        private void FillInSpace()
        {
            backgroundObjects = new List<ObjectInSpace>();
            foregroundObjects = new List<ObjectInSpace>();
            CreateStars();
            CreateAsteroids();
        }

        private void CreateStars()
        {
            Random rand = new Random();
            for (int i = 0; i < starsAmount; i++)
            {
                StarType starType = (StarType)rand.Next(1, 6);
                int starBrightness = rand.Next(1, 6);
                //StarType starType = StarType.Shiny;
                backgroundObjects.Add(new Star(
                    new Point(rand.Next(1, size.Width), rand.Next(1, size.Height)),
                    new Point(-rand.Next(1, i + 1), 0),
                    10, starType, starBrightness));
            }

        }

        private void CreateAsteroids()
        {
            Random rand = new Random();
            for (int i = 0; i < asteroidsAmount; i++)
            {
                foregroundObjects.Add(new Asteroid(
                    new Point(rand.Next(10, size.Width-10), rand.Next(10, size.Height - 10)),
                    new Point(-rand.Next(1, 20), rand.Next(20)),
                    10));
            }

        }

        public void MoveSpaceObjects()
        {
            MoveBackgroundObjects();
            MoveForegroundObjects();
        }

        public ObjectInSpace[] GetSpaceObjects()
        {
            ObjectInSpace[] objsInSpace = new ObjectInSpace[starsAmount + asteroidsAmount];
            int objsCounter = 0;
            for(int i = 0; i < backgroundObjects.Count; i++)
            {
                objsInSpace[objsCounter] = backgroundObjects[i];
                objsCounter++;
            }

            for (int i = 0; i < foregroundObjects.Count; i++)
            {
                objsInSpace[objsCounter] = foregroundObjects[i];
                objsCounter++;
            }

            return objsInSpace;
        }

        private void MoveBackgroundObjects()
        {
            foreach (var backgroundObject in backgroundObjects)
                backgroundObject.Move(size);
        }

        private void MoveForegroundObjects()
        {
            foreach (var foregroundObject in foregroundObjects)
                foregroundObject.Move(size);
        }

    }
}
