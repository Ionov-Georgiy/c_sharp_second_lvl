using AsteroidGame.GameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    class Space
    {
        private int starsAmount;
        private int asteroidsAmount;
        public Size SpaceSize => spaceSize;
        private Size spaceSize;
        private List<ObjectInSpace> backgroundObjects;
        private List<ObjectInSpace> foregroundObjects;
        public SpaceShip PlayerSpaceShip;

        public Space(Size size, int starsAmount, int asteroidsAmount)
        {
            this.spaceSize = size;
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
            CreateEnergyKit();
            CreateSpaceShip();
            ///////////////////////////////////////////
            foregroundObjects.Add(new Bullet(300, this)); //Временная затычка
        }

        private void CreateStars()
        {
            for (int i = 0; i < starsAmount; i++)
            {
                StarType starType = (StarType)StaticRandom.GetRandom(1, 6);
                int starBrightness = StaticRandom.GetRandom(1, 6);
                //StarType starType = StarType.Shiny;
                backgroundObjects.Add(new Star(
                    new Point(StaticRandom.GetRandom(1, SpaceSize.Width), StaticRandom.GetRandom(1, SpaceSize.Height)),
                    new Point(-StaticRandom.GetRandom(1, 20), 0),
                    10, 
                    starType, 
                    starBrightness, 
                    this));
            }

        }

        private void CreateAsteroids()
        {
            for (int i = 0; i < asteroidsAmount; i++)
            {
                Asteroid astr = new Asteroid(
                    new Point(SpaceSize.Width/*rand.Next(10, size.Width-10)*/, StaticRandom.GetRandom(10, SpaceSize.Height - 10)),
                    new Point(-StaticRandom.GetRandom(1, 20), StaticRandom.GetRandom(20)),
                    20,
                    this);
                astr.Collisionated += OnAsteroidCollision;
                foregroundObjects.Add(astr);
                Logger.Log(String.Format("[History] [ObjId:{0}] Объект {1} Position ({2}, {3}) создан.", astr.Id, astr.ToString(), astr.Position.X, astr.Position.Y));
            }
        }

        internal void ApplyScoreRecorderOnAsteroids(TDelegate<int> RecordScore)
        {
            foreach (ObjectInSpace foreObj in foregroundObjects)
            {
                if(foreObj is Asteroid astr)
                {
                    astr.recordScore = RecordScore;
                }
            }
        }

        private void CreateEnergyKit()
        {
            EnergyKit enKit = new EnergyKit(
                    new Point(StaticRandom.GetRandom(1, SpaceSize.Width), StaticRandom.GetRandom(1, SpaceSize.Height)),
                    new Point(-StaticRandom.GetRandom(1, 20), 0),
                    this);
            foregroundObjects.Add(enKit);
        }

        private void CreateSpaceShip()
        {
            PlayerSpaceShip = new SpaceShip(
                    new Point(10, 400),
                    new Point(5, 5),
                    new Size(20, 10),
                    this);
            PlayerSpaceShip.Destroyed += OnPlayerSpaceShipDestroyed;
            foregroundObjects.Add(PlayerSpaceShip);
        }

        private void OnAsteroidCollision(object sender, ObjectInSpaceEventArgs e)
        {
            Logger.Log(String.Format("Астероид {0} столкнулся c объектом {1}", (sender as ObjectInSpace).Id, (e.CollisionObject as ObjectInSpace).Id));
        }

        private void OnPlayerSpaceShipDestroyed(object sender, EventArgs e)
        {
            Game.GameOver = true;
        }

        public void MoveSpaceObjects()
        {
            MoveBackgroundObjects();
            MoveForegroundObjects();
            FindCollisionsDoCollisionConsequences();
        }

        public ObjectInSpace[] GetSpaceObjects()
        {
            List<ObjectInSpace> objsInSpace = new List<ObjectInSpace>();
            //ObjectInSpace[] objsInSpace = new ObjectInSpace[backgroundObjects.Count + foregroundObjects.Count];
            //int objsCounter = 0;
            //for(int i = 0; i < backgroundObjects.Count; i++)
            //{
            //    objsInSpace[objsCounter] = backgroundObjects[i];
            //    objsCounter++;
            //}

            //for (int i = 0; i < foregroundObjects.Count; i++)
            //{
            //    objsInSpace[objsCounter] = foregroundObjects[i];
            //    objsCounter++;
            //}

            objsInSpace.AddRange(GetBackgroundObjects());
            objsInSpace.AddRange(GetForegroundObjects());        

            return objsInSpace.ToArray();
        }

        public ObjectInSpace[] GetBackgroundObjects()
        {
            ObjectInSpace[] objsInSpace = new ObjectInSpace[backgroundObjects.Count];
            for (int i = 0; i < backgroundObjects.Count; i++)
            {
                objsInSpace[i] = backgroundObjects[i];
            }

            return objsInSpace;
        }

        public ObjectInSpace[] GetForegroundObjects()
        {
            ObjectInSpace[] objsInSpace = new ObjectInSpace[foregroundObjects.Count];
            for (int i = 0; i < foregroundObjects.Count; i++)
            {
                objsInSpace[i] = foregroundObjects[i];
            }

            return objsInSpace;
        }

        private void MoveBackgroundObjects()
        {
            foreach (var backgroundObject in backgroundObjects)
                backgroundObject.MoveInSpace();
        }

        private void MoveForegroundObjects()
        {
            foreach (var foregroundObject in foregroundObjects)
            {
                foregroundObject.MoveInSpace();
            }
        }

        private void FindCollisionsDoCollisionConsequences()
        {
            List<ICollisionable> ListOfCollisionableObjects = GetCollisionableObjects();
            foreach (var foregroundObject in foregroundObjects)
            {
                if (ListOfCollisionableObjects.Count > 0)
                {
                    if (foregroundObject is ICollisionable)
                    {
                        ICollisionable collisionableForegroundObject = foregroundObject;
                        ListOfCollisionableObjects.Remove(collisionableForegroundObject);
                        DoCollisions(collisionableForegroundObject, ListOfCollisionableObjects);
                    }
                }
            }
        }

        private List<ICollisionable> GetCollisionableObjects()
        {
            List<ICollisionable> ListOfCollisionableObjects = new List<ICollisionable>();
            foreach (var foregroundObject in foregroundObjects)
            {
                if(foregroundObject is ICollisionable)
                {
                    ListOfCollisionableObjects.Add(foregroundObject);
                }
            }
            return ListOfCollisionableObjects;
        }

        private void DoCollisions(ICollisionable foregroundObject, List<ICollisionable> ListOfObjects)
        {
            foreach (ICollisionable anotherForegroundObject in ListOfObjects)
            {
                if (anotherForegroundObject.CheckCollision(foregroundObject))
                {
                    ICollisionable temp;
                    if(foregroundObject is Asteroid)
                    {
                        temp = new Asteroid(foregroundObject.Rect.Location, foregroundObject.Direction, foregroundObject.Rect.Size.Width, this);
                    }
                    else if(foregroundObject is SpaceShip)
                    {
                        temp = new SpaceShip(foregroundObject.Rect.Location, foregroundObject.Direction, foregroundObject.Rect.Size, this);
                    }
                    else if (foregroundObject is EnergyKit)
                    {
                        temp = new EnergyKit(foregroundObject.Rect.Location, foregroundObject.Direction, this);
                    }
                    else
                    {
                        temp = new ObjectInSpace(foregroundObject.Rect.Location, foregroundObject.Direction, foregroundObject.Rect.Size, this);
                    }
                    foregroundObject.DoCollisionConsequences(anotherForegroundObject);
                    anotherForegroundObject.DoCollisionConsequences(temp);
                }
            }
        }

    }
}
